using Microsoft.AspNetCore.Mvc;
using MovieVote.Api.Discord;
using MovieVote.Exceptions;
using MovieVote.ViewModels;

namespace MovieVote.Controllers;

public class DiscordOauth : Controller
{
    [Route("oauth/discord")]
    public IActionResult Oauth(string? code)
    {
        if (code == null)
        {
            return View("ErrorPage", new ErrorPage { Error = "No 'code' parameter given." });
        }

        try
        {
            string accessToken = DiscordApi.GetAccessToken(code);
        }
        catch (ApiException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
            return View("ErrorPage", new ErrorPage { Error = e.Message });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
            return View("ErrorPage");
        }

        return RedirectToRoute("/");
    }
}