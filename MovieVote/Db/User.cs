using Microsoft.EntityFrameworkCore;

namespace MovieVote.Db;

/// <summary>
/// A row stored in the <c>users</c> table of the database.
/// </summary>
public class User
{
    /// <summary>The local user ID, which increments and is the same as the row number. Primary key.</summary>
    public int Id { get; set; }

    /// <summary>The users' username.</summary>
    public string Name { get; set; } = null!;

    /// <summary>The URL to the users' profile picture.</summary>
    public string Avatar { get; set; } = null!;

    /// <summary>The ID given by the OAuth provider.</summary>
    public string OAuthId { get; set; } = null!;

    /// <summary>The OAuth provider, for example Discord or GitHub.</summary>
    public string OAuthProvider { get; set; } = null!;

    /// <summary>The access token provided by the OAuth provider.</summary>
    public string OAuthAccessToken { get; set; } = null!;

    /// <summary>The refresh token to refresh the access token.</summary>
    public string OAuthRefreshToken { get; set; } = null!;

    /// <summary>The session ID, same as the session cookie.</summary>
    public string SessionId { get; set; } = null!;

    /// <summary>DateTime when the cookie and database user row expire.</summary>
    public DateTime SessionExpiry { get; set; }
}