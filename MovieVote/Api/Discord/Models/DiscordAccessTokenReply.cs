using Newtonsoft.Json;

namespace MovieVote.Api.Discord.Models;

public record DiscordAccessTokenReply
(
    [property: JsonProperty("access_token", Required = Required.Always)]
    string AccessToken,
    
    [property: JsonProperty("expires_in", Required = Required.Always)]
    int ExpiresIn,
    
    [property: JsonProperty("refresh_token", Required = Required.Always)]
    string RefreshToken,
    
    [property: JsonProperty("scope", Required = Required.Always)]
    string Scope,
    
    [property: JsonProperty("token_type", Required = Required.Always)]
    string TokenType
);