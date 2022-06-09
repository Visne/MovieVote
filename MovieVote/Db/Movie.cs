namespace MovieVote.Db;

public class Movie
{
    /*
          id           INTEGER PRIMARY KEY,
                    tmdb_id      TEXT NOT NULL UNIQUE, -- https://developers.themoviedb.org/3/movies/get-movie-details --
                    imdb_id      TEXT,                 -- starts with 'tt' followed by 7 digits
                    title        TEXT NOT NULL,
                    release_date TEXT NOT NULL,
                    poster_path  TEXT,
                    overview     TEXT,
                    tagline      TEXT,
                    genres       TEXT,
                    date_added   INTEGER NOT NULL
     */
    
    public int Id { get; set; }
    public string TmdbId { get; set; } = null!;
    public string? ImdbId { get; set; }
    public string Title { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }
    public string? Poster { get; set; }
    public string? Overview { get; set; }
    public string? Tagline { get; set; }
    public DateTime AddedOn { get; set; }
}