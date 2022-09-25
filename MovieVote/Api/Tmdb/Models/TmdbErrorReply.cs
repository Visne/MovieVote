using Newtonsoft.Json;

namespace MovieVote.Api.Tmdb.Models;


/// <summary>
/// An error reply from The Movie Database.
/// </summary>
/// <param name="StatusMessage">Status message provided by TMDB.</param>
/// <param name="StatusCode">Error status code, defined by TMDB.</param>
public record TmdbErrorReply
(
    [property: JsonProperty("status_message")]
    string StatusMessage,

    [property: JsonProperty("status_code")]
    int StatusCode
);