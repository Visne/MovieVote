using Newtonsoft.Json;

namespace MovieVote.Configuration;

public class TmdbConfig
{
    /// <summary>
    /// The Movie Database API key.
    /// </summary>
    [JsonProperty(PropertyName = "api_key", Required = Required.Always)]
    public string ApiKey =  null!;
}