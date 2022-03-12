using System.Diagnostics.Contracts;
using MovieVote.Api.Discord.Json;
using MovieVote.Exceptions;

namespace MovieVote.Api.Discord;

public static class DiscordApi
{
    /// <summary>
    ///     Get a Discord OAuth access token from a code.
    /// </summary>
    /// <exception cref="ApiException"></exception>
    [Pure]
    public static string GetAccessToken(string code)
    {
        using var client = new HttpClient();

        // Send code to request access token
        var resp = client.Send(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://discord.com/api/oauth2/token"),
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", Program.Config.Discord.ClientId },
                { "client_secret", Program.Config.Discord.ClientSecret },
                { "redirect_uri", Program.Config.Discord.RedirectUri },
                { "code", code },
                { "grant_type", "authorization_code" },
            }),
        });

        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine("Something went wrong while getting token:");
            var errorReply = resp.Content.ReadFromJsonAsync<DiscordErrorReply>().Result;

            if (errorReply == null)
            {
                throw new ApiException("Failed to deserialize an error:\n" + resp.Content.ReadAsStringAsync().Result);
            }
            
            throw new ApiException($"{errorReply.Error}: {errorReply.ErrorDescription}");
        }

        var tokenReply = resp.Content.ReadFromJsonAsync<DiscordAccessTokenReply>().Result;

        if (tokenReply?.AccessToken == null)
        {
            throw new ApiException("Failed to deserialize token reply:\n" + resp.Content.ReadAsStringAsync().Result);
        }

        return tokenReply.AccessToken;
    }
}