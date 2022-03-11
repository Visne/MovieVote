using System.Diagnostics.Contracts;
using Microsoft.Extensions.Primitives;
using MovieVote.Api.Discord.Json;
using MovieVote.Configuration;

namespace MovieVote.Api.Discord;

public static class Discord
{
    [Pure]
    public static async Task<string> GetAccessToken(HttpContext context)
    {
        using var client = new HttpClient();

        if (context.Request.Query["code"] == StringValues.Empty || context.Request.Query["code"] == string.Empty)
        {
            throw new Exception("No 'code' given in GET params.");
        }
        
        var req = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://discord.com/api/oauth2/token"),
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", Program.Config.Discord.ClientId },
                { "client_secret", Program.Config.Discord.ClientSecret },
                { "redirect_uri", Program.Config.Discord.RedirectUri },
                { "code", context.Request.Query["code"] },
                { "grant_type", "authorization_code" },
            }),
        };

        var resp = await client.SendAsync(req);

        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Something went wrong while getting token:");
            var errorReply = await resp.Content.ReadFromJsonAsync<DiscordErrorReply>();

            if (errorReply == null)
            {
                throw new Exception("Failed to deserialize an error:\n" + resp.Content.ReadAsStringAsync().Result);
            }
            
            throw new Exception($"{errorReply.Error}: {errorReply.ErrorDescription}");
        }

        var tokenReply = await resp.Content.ReadFromJsonAsync<DiscordAccessTokenReply>();

        if (tokenReply?.AccessToken == null)
        {
            throw new Exception("Failed to deserialize token reply:\n" + resp.Content.ReadAsStringAsync().Result);
        }

        return tokenReply.AccessToken;
    }
}