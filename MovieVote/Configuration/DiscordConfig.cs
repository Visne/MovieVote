using Newtonsoft.Json;

namespace MovieVote.Configuration;

public class DiscordConfig
{
    [JsonProperty(PropertyName = "client_id", Required = Required.Always)]
    public string ClientId =  null!;

    [JsonProperty(PropertyName = "client_secret", Required = Required.Always)]
    public string ClientSecret = null!;

    [JsonProperty(PropertyName = "redirect_uri", Required = Required.Always)]
    public string RedirectUri = null!;
}