using Newtonsoft.Json;

namespace MovieVote.Api.Tmdb.Models;

public class TmdbGenre
{
    [JsonProperty("id")]
    public int? Id;

    [JsonProperty("name")]
    public string? Name;
}