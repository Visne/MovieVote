using System.Net;
using JetBrains.Annotations;
using MovieVote.Api.Tmdb.Models;
using Newtonsoft.Json;
using MovieVote.Extensions;

namespace MovieVote.Api.Tmdb;

public static class TmdbApi
{
    [MustUseReturnValue]
    public static async Task<TmdbMovieReply?> GetMovieDetails(int id)
    {
        using var client = new HttpClient();

        // Send code to request access token
        var resp = await client.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.themoviedb.org/3/movie/{id}"),
            Headers =
            {
                { "Authorization", $"Bearer {Program.Config.Tmdb.ApiKey}" },
            },
        });

        if (resp.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!resp.IsSuccessStatusCode)
        {
            var reply = await new JsonSerializer().Deserialize<TmdbErrorReply>(resp.Content);

            if (reply == null)
            {
                throw new Exception($"Failed to deserialize an error:\n{resp.Content.ReadAsStringAsync().Result}");
            }
            
            throw new Exception($"{reply.StatusCode}: {reply.StatusMessage}");
        }

        var movie = await new JsonSerializer().Deserialize<TmdbMovieReply>(resp.Content);

        if (movie == null)
        {
            throw new Exception($"Failed to deserialzie movie reply:\n{resp.Content.ReadAsStringAsync().Result}");
        }

        return movie;
    }
}