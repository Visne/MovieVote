using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using MovieVote.Api.Discord.Models;
using MovieVote.Exceptions;
using MovieVote.Extensions;
using Newtonsoft.Json;

namespace MovieVote.Api.Discord;

public static class DiscordApi
{
    /// <summary>
    ///     Get a Discord OAuth access token from a code.
    /// </summary>
    /// <exception cref="ApiException"></exception>
    [Pure]
    public static async Task<DiscordAccessTokenReply> GetAccessToken(string code)
    {
        using var client = new HttpClient();

        // Send code to request access token
        var resp = await client.SendAsync(new HttpRequestMessage
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
            var errorReply = await new JsonSerializer().Deserialize<DiscordErrorReply>(resp.Content);

            if (errorReply == null)
            {
                throw new Exception("Failed to deserialize an error:\n" + resp.Content.ReadAsStringAsync().Result);
            }
            
            throw new Exception($"{errorReply.Error}: {errorReply.ErrorDescription}");
        }
        
        var tokenReply = await new JsonSerializer().Deserialize<DiscordAccessTokenReply>(resp.Content);

        if (tokenReply?.AccessToken == null)
        {
            string str = resp.Content.ReadAsStringAsync().Result;
            str = Regex.Replace(str, @"""access_token"":\s*""(.{5}).*?""", @"""access_token"": ""$1*************************""");
            str = Regex.Replace(str, @"""refresh_token"":\s*""(.{5}).*?""", @"""refresh_token"": ""$1*************************""");
            
            throw new ApiException("Failed to deserialize token reply:\n" + str);
        }

        return tokenReply;
    }

    public static async Task<DiscordUser> GetDiscordUserData(string accessToken)
    {
        using var client = new HttpClient();

        // Send code to request access token
        var resp = await client.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://discord.com/api/v9/users/@me"),
            Headers = { { "Authorization", $"Bearer {accessToken}" } },
        });

        DiscordUser? userData = await new JsonSerializer().Deserialize<DiscordUser>(resp.Content);

        if (userData == null)
        {
            throw new ApiException("Returned user data is null.");
        }

        return userData;
    }
}