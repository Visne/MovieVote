using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using MovieVote.Api.Discord;
using MovieVote.Api.Discord.Json;
using MovieVote.Db;
using MovieVote.Exceptions;
using MovieVote.Views.Shared;
using static Microsoft.AspNetCore.WebUtilities.WebEncoders;

namespace MovieVote.Controllers;

public class DiscordOauthController : Controller
{
    [Route("oauth/discord")]
    public async Task<IActionResult> OAuth(string? code)
    {
        if (code == null)
        {
            return View("Error", new ErrorModel { ErrorMessage = "No 'code' parameter given." });
        }

        try
        {
            var reply = await DiscordApi.GetAccessToken(code);
            string accessToken = reply.AccessToken;
            string refreshToken = reply.RefreshToken;
            DiscordUser userData = await DiscordApi.GetDiscordUserData(accessToken);
            TimeSpan expiry = TimeSpan.FromSeconds(reply.ExpiresIn);
            //TimeSpan expiry = TimeSpan.FromMinutes(Program.Config.Expiry);
            string sessionId = GenerateSessionId();
            
            Database.InsertDiscordUserData(userData, accessToken, refreshToken, sessionId, expiry);

            // Add session cookie
            Response.Cookies.Append("session", sessionId, new CookieOptions
            {
                MaxAge = expiry,
                Secure = true,
                HttpOnly = false,
                Path = "/",
            });
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception during OAuth:");
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);

            // Safe to expose error message to user
            if (e is ApiException)
            {
                return View("Error", new ErrorModel { ErrorMessage = e.Message });
            }

            return View("Error");
        }

        return Redirect("/");
    }

    private static string GenerateSessionId()
    {
        return Base64UrlEncode(RandomNumberGenerator.GetBytes(16));
    }
}