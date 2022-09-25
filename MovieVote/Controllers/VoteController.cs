using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc;
using MovieVote.Db;
using Newtonsoft.Json;

namespace MovieVote.Controllers;

public class VoteController : ControllerBase
{
    private string? _sessionId;
    private static readonly List<VoteController> ConnectedClients = new();
    private readonly BufferBlock<(int MovieId, bool IsUpvote, int NewCount, bool IsUs)> _voteQueue = new();
    private static int _total = 0;
    private readonly DatabaseContext _ctx;
    
    public VoteController(DatabaseContext ctx) => _ctx = ctx;
    
    [HttpGet("/vote")]
    public async Task OnGet()
    {
        HttpContext.Request.Cookies.TryGetValue("session", out _sessionId);

        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        using var ws = await HttpContext.WebSockets.AcceptWebSocketAsync();

        if (!ConnectedClients.Contains(this))
        {
            // Absolutely no clue if this is high IQ or stupid af
            _total += 1;
            ConnectedClients.Add(this);
        }

        try
        {
            await Task.WhenAll(Send(ws), Receive(ws));
        }
        catch (WebSocketException e)
        {
            // Client closed tab or refreshed
            if (e.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely) return;

            throw;
        }
        finally
        {
            _total -= 1;
            ConnectedClients.Remove(this);
        }
    }

    private async Task Send(WebSocket ws)
    {
        while (ws.CloseStatus is null or WebSocketCloseStatus.Empty)
        {
            var vote = await _voteQueue.ReceiveAsync();
            
            string isUpvote = vote.IsUpvote.ToString().ToLowerInvariant();
            string isUs = vote.IsUs.ToString().ToLowerInvariant();
            string msg = @$"{{""m"":{vote.MovieId},""u"":{isUpvote},""v"":{vote.NewCount},""s"":{isUs}}}";
            
            // Check if the socket is still open
            // If not, it doesn't matter if we drop the vote
            if (ws.State != WebSocketState.Open) continue;
            
            await ws.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(msg)),
                               WebSocketMessageType.Text,
                               true,
                               CancellationToken.None);
        }
    }

    private async Task Receive(WebSocket ws)
    {
        while (ws.CloseStatus is null or WebSocketCloseStatus.Empty)
        {
            var (message, response) = await ReceiveFullMessage(ws);

            if (response.MessageType == WebSocketMessageType.Close) break;

            var reader = new StringReader(Encoding.UTF8.GetString(message.ToArray()));
            var vote = new JsonSerializer().Deserialize<VoteMessage>(new JsonTextReader(reader));

            // Invalid vote
            if (vote?.SessionId == null) continue;

            // TODO: Toast user depending on return value
            bool? status = await _ctx.UpdateVote(vote.SessionId, vote.MovieId, vote.IsUpvote);
            int newCount = await _ctx.GetVotes(vote.MovieId);

            if (status == true)
            {
                foreach (var client in ConnectedClients)
                {
                    // Do not send to ourselves
                    //if (client == this) continue;

                    client._voteQueue.Post((vote.MovieId, vote.IsUpvote, newCount, client._sessionId == _sessionId));
                }
            }
        }
    }
    
    private static async Task<(IEnumerable<byte> Message, WebSocketReceiveResult FinalResponse)> ReceiveFullMessage(
        WebSocket ws, CancellationToken token = default)
    {
        WebSocketReceiveResult response;
        var message = new List<byte>();
        var buffer = new byte[1024];
        
        do
        {
            response = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), token);
            message.AddRange(new ArraySegment<byte>(buffer, 0, response.Count));
        } while (!response.EndOfMessage);

        return (message, response);
    }
 
    private class VoteMessage
    {
        [JsonProperty(PropertyName = "u", Required = Required.Always)]
        public bool IsUpvote = false;

        [JsonProperty(PropertyName = "m", Required = Required.Always)]
        public int MovieId = 0;
        
        [JsonProperty(PropertyName = "s")]
        public string? SessionId;
    }
}
