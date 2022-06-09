using Newtonsoft.Json;

namespace MovieVote.Configuration;

public class DiscordConfig
{
    /// <summary>
    /// Discord application Client ID.
    /// </summary>
    [JsonProperty(PropertyName = "client_id", Required = Required.Always)]
    public string ClientId =  null!;

    /// <summary>
    /// Discord application Client Secret.
    /// </summary>
    [JsonProperty(PropertyName = "client_secret", Required = Required.Always)]
    public string ClientSecret = null!;

    /// <summary>
    /// Discord application OAuth redirect URL.
    /// </summary>
    [JsonProperty(PropertyName = "redirect_uri", Required = Required.Always)]
    public string RedirectUri = null!;
}