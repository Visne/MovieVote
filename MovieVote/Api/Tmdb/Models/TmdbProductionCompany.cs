using Newtonsoft.Json;

namespace MovieVote.Api.Tmdb.Models;

public class TmdbProductionCompany
{
    [JsonProperty("id")]
    public int? Id;

    [JsonProperty("logo_path")]
    public string? LogoPath;

    [JsonProperty("name")]
    public string? Name;

    [JsonProperty("origin_country")]
    public string? OriginCountry;
}