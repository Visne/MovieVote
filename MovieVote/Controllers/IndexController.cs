using Microsoft.AspNetCore.Mvc;
using MovieVote.Views.Index;
using MovieVote.Views.Shared;

namespace MovieVote.Controllers;

public class IndexController : Controller
{
    [Route("/")]
    public IActionResult OnGet()
    {
        return View("Index", new IndexModel
        {
            Movies =
            {
                new MovieModel("", "b", 2020, null),
                new MovieModel("e", "f", 1999, null),
                new MovieModel("g", "h", null, null),
            },
        });
    }
}
