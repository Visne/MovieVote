namespace MovieVote.Views.Shared;

public class MovieModel
{
    public string? Title;
    public string? Description;
    public int? Year;
    public string? PosterUrl;

    public MovieModel() { }
    
    public MovieModel(string? title, string? description, int? year, string? posterUrl)
    {
        Title = title;
        Description = description;
        Year = year;
        PosterUrl = posterUrl;
    }
}