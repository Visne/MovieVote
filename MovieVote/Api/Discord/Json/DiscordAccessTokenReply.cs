using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace MovieVote.Api.Discord.Json;

public class DiscordAccessTokenReply
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; [UsedImplicitly] set; }
    
    [JsonPropertyName("expires_in")]
    public int? ExpiresIn { get; [UsedImplicitly] set; }
    
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; [UsedImplicitly] set; }
    
    [JsonPropertyName("scope")]
    public string? Scope { get; [UsedImplicitly] set; }
    
    [JsonPropertyName("token_type")]
    public string? TokenType { get; [UsedImplicitly] set; }
}