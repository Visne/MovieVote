using Microsoft.AspNetCore.Mvc;
using MovieVote.Api.Tmdb;
using MovieVote.Db;
using MovieVote.Exceptions;
using MovieVote.Views.Add;
using MovieVote.Views.Shared;

namespace MovieVote.Controllers;

public class AddController : Controller
{
    private readonly DatabaseContext _ctx;
    public AddController(DatabaseContext ctx) => _ctx = ctx;

    [Route("/add")]
    public async Task<IActionResult> OnGet(string? id, int? add)
    {
        // User is not logged in, return to main page
        if (!HttpContext.Request.Cookies.TryGetValue("session", out string? sessionId) || sessionId == null)
        {
            return Redirect("/");
        }

        // If both add and id are given, something must be wrong
        if (add != null && id != null)
        {
            return View("Error", new ErrorModel("Both 'get' and 'id' params given."));
        }
        
        // If an ID is provided, get its details and ask the user if it is correct
        if (id != null)
        {
            // TODO: Also allowed URLs and IMDb IDs
            // TODO: Search functionality
            if (!int.TryParse(id, out int validId))
            {
                return View("Error", new ErrorModel("Invalid ID given."));
            }
            
            try
            {
                var movie = await TmdbApi.GetMovieDetails(validId);

                if (movie == null)
                {
                    return View("Add", new AddModel((false, "No movie found with this ID!")));
                }

                if (movie.Title == null)
                {
                    throw new Exception($"Movie {movie.Id} has no title!");
                }

                ViewBag.HideUpvote = true;
                
                return View("Add", new AddModel
                {
                    Id = movie.Id,
                    Movie = new Movie
                    {
                        Title = movie.Title,
                        Overview = movie.Overview,
                        ReleaseDate = DateOnly.TryParse(movie.ReleaseDate, out DateOnly date) ? date : null,
                        Poster = $"https://image.tmdb.org/t/p/w500{movie.PosterPath}",
                    },
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception during /add:");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                // Safe to expose error message to user
                if (e is ApiException)
                {
                    return View("Error", new ErrorModel(e.Message));
                }

                return View("Error");
            }
        }
        
        if (add != null)
        {
            var user = _ctx.GetStoredUserData(sessionId);
            
            if (user == null)
            {
                return View("Error", new ErrorModel("User not found in the database, try logging out and back in."));
            }
            
            var movie = await TmdbApi.GetMovieDetails(add.Value);

            if (movie == null)
            {
                return View("Error", new ErrorModel("No movie found with this ID!"));
            }

            try
            {
                bool added = await _ctx.AddMovie(movie, user);

                if (!added)
                {
                    // TODO: This should already be shown on `id` stage
                    return View("Add", new AddModel((false, "Movie is already in the list!")));
                }

                return View("Add", new AddModel((true, $"{movie.Title} has been added to the list!")));
            }
            catch (Exception e)
            {
                // TODO: Duplicate code
                Console.WriteLine("Exception during /add:");
                Console.WriteLine(e.Message);   
                Console.WriteLine(e.StackTrace);

                // Safe to expose error message to user
                if (e is ApiException)
                {
                    return View("Error", new ErrorModel(e.Message));
                }

                return View("Error");
            }
        }

        return View("Add");
    }
}