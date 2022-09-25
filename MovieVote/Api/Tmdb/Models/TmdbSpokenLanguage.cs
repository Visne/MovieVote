using Newtonsoft.Json;

namespace MovieVote.Api.Tmdb.Models;

public class TmdbSpokenLanguage
{
    /// <summary>
    /// Language code in ISO 639-1 format.
    /// </summary>
    [JsonProperty("iso_639_1")]
    public string? LanguageCode;

    [JsonProperty("name")]
    public string? Name;
}