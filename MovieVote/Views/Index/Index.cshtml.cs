using MovieVote.Db;

namespace MovieVote.Views.Index;

public class IndexModel
{
    public List<Movie> Movies;
    
    public IndexModel(List<Movie> movies)
    {
        Movies = movies;
    }
}