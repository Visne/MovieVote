using System.Data.SQLite;

namespace MovieVote.Db;

public static partial class Database
{
    public static void InitializeDb()
    {
        using var conn = new SQLiteConnection(new SQLiteConnectionStringBuilder
        {
            DataSource = Program.Config.Database,
        }.ToString());
        
        conn.Open();

        var createUsers = conn.CreateCommand();
        createUsers.CommandText =
            @"CREATE TABLE IF NOT EXISTS users
                    (
                        id                 INTEGER PRIMARY KEY, -- Primary ID, alias for ROWID --
                        oauth_id           INTEGER NOT NULL,    -- ID provided by OAuth provider --
                        oauth_provider     TEXT    NOT NULL,    -- OAuth provider --
                        oauth_access_token TEXT,                -- Access token for OAuth provider --
                        session_id         TEXT,                -- Session ID cookie, unique and unguessable --
                        session_expiry     INTEGER,             -- Expiry of session, in milliseconds since 1 Jan 1970 --
                        UNIQUE (oauth_id, oauth_provider)       -- Combination of provider and ID must be unique --
                    );
                ";
        createUsers.ExecuteNonQuery();
        
        var createMovies = conn.CreateCommand();
        createMovies.CommandText =
                @"CREATE TABLE IF NOT EXISTS movies
                (
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
                );
            ";
        createMovies.ExecuteNonQuery();
    }
}