using MovieVote.Exceptions;
using Newtonsoft.Json;

namespace MovieVote.Api.Tmdb;

public static class TmdbApi
{
    public static async void GetMovieDetails(int id)
    {
        using var client = new HttpClient();

        // Send code to request access token
        var resp = await client.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"https://api.themoviedb.org/3/movie/{id}"),
            Headers =
            {
                //{ "Authorization", Program.Config.Tmdb.ApiKey },
            },
        });

        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Something went wrong while getting token:");
            /*var errorReply = await new JsonSerializer().Deserialize<DiscordErrorReply>(resp.Content);

            if (errorReply == null)
            {
                throw new ApiException("Failed to deserialize an error:\n" + resp.Content.ReadAsStringAsync().Result);
            }*/
            
            throw new ApiException($"Failed to get movie details.");
        }
        
        //var tokenReply = await new JsonSerializer().Deserialize<dynamic>(resp.Content);

        

        //return tokenReply;
    }
}