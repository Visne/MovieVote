using Microsoft.EntityFrameworkCore;
using MovieVote.Api.Discord.Json;

namespace MovieVote.Db;

public static partial class Database
{
    /// <summary>
    /// Adds a Discord user to the database.
    /// </summary>
    /// <param name="userData">User data given by Discord.</param>
    /// <param name="accessToken">Access token given by Discord.</param>
    /// <param name="refreshToken">Refresh token given by Discord.</param>
    /// <param name="sessionId">Unique session ID.</param>
    /// <param name="expiry"><see cref="TimeSpan"/> until expiry.</param>
    public static void InsertDiscordUserData(DiscordUser userData, string accessToken, string refreshToken, string sessionId, TimeSpan expiry)
    {
        using var ctx = new DatabaseContext();

        ctx.Users.Upsert(new User
           {
               Name = userData.Username,
               Avatar = userData.Avatar != null
                   ? $"https://cdn.discordapp.com/avatars/{userData.Id}/{userData.Avatar}.png?size=512"
                   : "https://cdn.discordapp.com/embed/avatars/0.png",
               OAuthId = userData.Id,
               OAuthProvider = "discord",
               OAuthAccessToken = accessToken,
               OAuthRefreshToken = refreshToken,
               SessionId = sessionId,
               SessionExpiry = DateTime.UtcNow + expiry,
           })
           .On(u => new { u.OAuthId, u.OAuthProvider })
           .Run();
    }

    public static User? GetStoredUserData(string sessionId)
    {
        using var ctx = new DatabaseContext();

        return ctx.Users.SingleOrDefault(u => u.SessionId == sessionId && DateTime.UtcNow < u.SessionExpiry);
    }
}