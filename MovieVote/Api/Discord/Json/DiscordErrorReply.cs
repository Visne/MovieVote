using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace MovieVote.Api.Discord.Json;

public class DiscordErrorReply
{
    [JsonPropertyName("error")]
    public string? Error { get; [UsedImplicitly] set; }

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; [UsedImplicitly] set; }
}