using Newtonsoft.Json;

namespace MovieVote.Api.Tmdb.Models;

public class TmdbProductionCountry
{
    /// <summary>
    /// Country code in ISO 3166-1 format.
    /// </summary>
    [JsonProperty("iso_3166_1")]
    public string? CountryCode;

    [JsonProperty("name")]
    public string? Name;
}