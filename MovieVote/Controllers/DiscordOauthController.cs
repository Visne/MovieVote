using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using MovieVote.Api.Discord;
using MovieVote.Api.Discord.Models;
using MovieVote.Db;
using MovieVote.Exceptions;
using MovieVote.Views.Shared;
using static Microsoft.AspNetCore.WebUtilities.WebEncoders;

namespace MovieVote.Controllers;

public class DiscordOauthController : Controller
{
    private readonly DatabaseContext _ctx;
    
    public DiscordOauthController(DatabaseContext ctx) => _ctx = ctx;
    
    [Route("oauth/discord")]
    public async Task<IActionResult> OAuth(string? code)
    {
        if (code == null)
        {
            return View("Error", new ErrorModel("No 'code' parameter given."));
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
            
            _ctx.InsertDiscordUserData(userData, accessToken, refreshToken, sessionId, expiry);

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
            Console.WriteLine("Exception during /oauth/discord:");
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);

            // Safe to expose error message to user
            if (e is ApiException)
            {
                return View("Error", new ErrorModel(e.Message));
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