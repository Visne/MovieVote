using JetBrains.Annotations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MovieVote.Api.Discord.Models;
using MovieVote.Api.Tmdb.Models;

namespace MovieVote.Db;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Vote> Votes => Set<Vote>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={Program.Config.Database}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Make (OAuthId, OAuthProvider) unique
        modelBuilder.Entity<User>()
                    .HasIndex(u => new { u.OAuthId, u.OAuthProvider })
                    .IsUnique();

        // Make TmdbId unique
        modelBuilder.Entity<Movie>()
                    .HasIndex(m => m.TmdbId)
                    .IsUnique();

        // Make (MovieId, VoterId) unique
        modelBuilder.Entity<Vote>()
                    .HasIndex(v => new { v.MovieId, v.VoterId })
                    .IsUnique();
    }
    
    /// <summary>
    /// Adds a Discord user to the database.
    /// </summary>
    /// <param name="userData">User data given by Discord.</param>
    /// <param name="accessToken">Access token given by Discord.</param>
    /// <param name="refreshToken">Refresh token given by Discord.</param>
    /// <param name="sessionId">Unique session ID.</param>
    /// <param name="expiry"><see cref="TimeSpan"/> until expiry.</param>
    public void InsertDiscordUserData(DiscordUser userData, string accessToken, string refreshToken, string sessionId, TimeSpan expiry)
    {
        Users.Upsert(new User
           {
               Name = userData.Username,
               Avatar = userData.Avatar == null
                   ? "https://cdn.discordapp.com/embed/avatars/0.png"
                   : $"https://cdn.discordapp.com/avatars/{userData.Id}/{userData.Avatar}.png?size=512",
               OAuthId = userData.Id,
               OAuthProvider = "discord",
               OAuthAccessToken = accessToken,
               OAuthRefreshToken = refreshToken,
               SessionId = sessionId,
               SessionExpiry = DateTime.UtcNow + expiry,
           })
           .On(u => new { u.OAuthId, u.OAuthProvider })
           .Run();
    }

    [MustUseReturnValue]
    public User? GetStoredUserData(string sessionId)
    {
        return Users.SingleOrDefault(u => u.SessionId == sessionId && DateTime.UtcNow < u.SessionExpiry);
    }

    public async Task<bool> AddMovie(TmdbMovieReply movie, User addedBy)
    {
        if (movie.Id == null)
            throw new Exception("Movie ID was null.");

        // Block movies without a title from being added
        if (movie.Title == null)
            throw new Exception("Movie title was null");

        try
        {
            Console.WriteLine(addedBy.Id);
            Console.WriteLine(movie.Id);
            
            Movies.Add(new Movie
            {
                TmdbId = movie.Id.Value,
                ImdbId = movie.ImdbId,
                Title = movie.Title,
                ReleaseDate = DateOnly.TryParse(movie.ReleaseDate, out var date) ? date : null,
                Poster = $"https://image.tmdb.org/t/p/w500{movie.PosterPath}",
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                AddedOn = DateTime.UtcNow,
                AddedBy = addedBy,
            });
            
            await SaveChangesAsync();

            return true;
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException { SqliteErrorCode: 19 })
        {
            Console.WriteLine(e.InnerException.Message);
            // If the movie is already in the database, return false, else throw original error
            return false;
        }
        catch (Exception e)
        {
            if (e.InnerException != null)
            {
                Console.WriteLine(e.InnerException.Message);
                // TODO: Custom error message
                // TODO: SqliteErrorCode: 5 database is locked
                return false;
            }
            
            Console.WriteLine(e.Message);
            return false;
        }
    }

    /// <summary>
    /// Update a movie vote in the database. 
    /// </summary>
    /// <returns>True if the update was saved, false if the update failed, null if the database was already up to date.</returns>
    public async Task<bool?> UpdateVote(string sessionId, int movieId, bool isUpvote)
    {
        var voter = GetStoredUserData(sessionId);
        var movie = Movies.SingleOrDefault(m => m.Id == movieId);

        // Invalid vote, return
        if (voter == null || movie == null) return false;

        var toChange = await Votes.SingleOrDefaultAsync(v => v.MovieId == movieId && v.VoterId == voter.Id);
        
        if (isUpvote && toChange == null)
        {
            Votes.Add(new Vote
            {
                MovieId = movieId,
                VoterId = voter.Id,
            });
        }
        else if (!isUpvote && toChange != null)
        {
            Votes.Remove(toChange);
        }
        else
        {
            // Database state already corresponds with request
            return null;
        }

        await SaveChangesAsync();
        return true;
    }

    public async Task<int> GetVotes(int movieId)
    {
        return await Votes.CountAsync(v => v.MovieId == movieId);
    }
}