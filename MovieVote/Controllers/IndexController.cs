using Microsoft.AspNetCore.Mvc;
using MovieVote.Db;
using MovieVote.Views.Index;

namespace MovieVote.Controllers;

public class IndexController : Controller
{
    private readonly DatabaseContext _ctx;
    
    public IndexController(DatabaseContext ctx) => _ctx = ctx;
    
    [Route("/")]
    public IActionResult OnGet()
    {
        return View("Index", new IndexModel(_ctx.Movies.ToList()));
    }
}
