using Newtonsoft.Json;

namespace MovieVote.Api.Discord.Json;

/// <summary>
/// An error reply from Discord.
/// </summary>
/// <param name="Error">Name of the error.</param>
/// <param name="ErrorDescription">Description of the error.</param>
public record DiscordErrorReply
(
    [property: JsonProperty("error")]
    string? Error,

    [property: JsonProperty("error_description")]
    string? ErrorDescription
);