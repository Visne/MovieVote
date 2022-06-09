using Microsoft.AspNetCore.Mvc;
using MovieVote.Views.Add;
using MovieVote.Views.Shared;

namespace MovieVote.Controllers;

public class AddController : Controller
{
    [Route("/add")]
    public IActionResult OnGet(string? id, string? add)
    {
        // User is not logged in, return to main page
        if (!HttpContext.Request.Cookies.ContainsKey("session"))
        {
            return Redirect("/");
        }

        // If both add and id are given, something must be wrong
        if (add != null && id != null)
        {
            return View("Error", new ErrorModel { ErrorMessage = "Both 'get' and 'id' params given." });
        }
        
        if (id != null)
        {
            // TODO: Get movie data from ID
            return View("Add", new AddModel
            {
                Id = id,
                Movie = new MovieModel
                {
                    Title = "a",
                    Description = "hi hello hello hi",
                    Year = 2000,
                    PosterUrl = "images/missing.png",
                },
            });
        }
        
        if (add != null)
        {
            // TODO: Add movie to the database
            return View("Add", new AddModel { Result = (true, "Success!") });
        }

        return View("Add");
    }
}