using Microsoft.AspNetCore.Mvc;
using MovieVote.Api.Discord;

namespace MovieVote.Pages.OAuth;

public class DiscordPage
{
    public async Task<IActionResult> OnGet()
    {
        try
        {
            //string token = await Discord.GetAccessToken(context);
            Console.WriteLine(token);
            //return RedirectToPageResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
            //return RazorPage()
        }
    }
}