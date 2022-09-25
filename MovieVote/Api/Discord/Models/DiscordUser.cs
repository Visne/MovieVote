using Newtonsoft.Json;

namespace MovieVote.Api.Discord.Models;

/// <summary>
/// Discord user data.
/// </summary>
/// <param name="Id">Discord user ID.</param>
/// <param name="Username">Discord global username.</param>
/// <param name="Discriminator">Four numbers behind username to differentiate between users with the same username.</param>
/// <param name="Avatar">User profile picture. Not a direct link, but name of the image without file extension.</param>
/// <param name="Bot">Whether the user is a bot.</param>
/// <param name="System"></param>
/// <param name="MfaEnabled">Whether the user has MFA enabled.</param>
/// <param name="Banner">User profile banner image.</param>
/// <param name="AccentColor"></param>
/// <param name="Locale"></param>
/// <param name="Verified"></param>
/// <param name="Email">The users' email address.</param>
/// <param name="Flags"></param>
/// <param name="PremiumType"></param>
/// <param name="PublicFlags"></param>
public record DiscordUser
(
    [property: JsonProperty("id", Required = Required.Always)]
    string Id,
    
    [property: JsonProperty("username", Required = Required.Always)]
    string Username,
    
    [property: JsonProperty("discriminator", Required = Required.Always)]
    string Discriminator,
    
    [property: JsonProperty("avatar")]
    string? Avatar,
    
    [property: JsonProperty("bot")]
    bool? Bot,
    
    [property: JsonProperty("system")]
    bool? System,
    
    [property: JsonProperty("mfa_enabled")]
    bool? MfaEnabled,
    
    [property: JsonProperty("banner")]
    string? Banner,
    
    [property: JsonProperty("accent_color")]
    int? AccentColor,
    
    [property: JsonProperty("locale")]
    string? Locale,
    
    [property: JsonProperty("verified")]
    bool? Verified,
    
    [property: JsonProperty("email")]
    string? Email,
    
    [property: JsonProperty("flags")]
    int? Flags,
    
    [property: JsonProperty("premium_type")]
    int? PremiumType,
    
    [property: JsonProperty("public_flags")]
    int? PublicFlags
);
