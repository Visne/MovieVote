using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieVote.Pages;

public class IndexModel : PageModel
{
    public static readonly List<Movie> Movies = new();

    public void OnGet()
    {
        Movies.Clear();
        Movies.Add(new Movie("", "b", 2020, "cd"));
        Movies.Add(new Movie("e", "f", 1999, null));
        Movies.Add(new Movie("g", "h", null, "cdd"));
    }
}

public class Movie
{
    public readonly string? Title;
    public readonly string? Description;
    public int? Year;
    public string? PosterUrl;

    public Movie(string? title, string? description, int? year, string? posterUrl)
    {
        Title = title;
        Description = description;
        Year = year;
        PosterUrl = posterUrl;
    }
}