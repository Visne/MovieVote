using Microsoft.EntityFrameworkCore;

namespace MovieVote.Db;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Movie> Movies => Set<Movie>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={Program.Config.Database}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Make (OAuthId, OAuthProvider) unique
        modelBuilder.Entity<User>()
                    .HasIndex(u => new { u.OAuthId , u.OAuthProvider })
                    .IsUnique();
    }
}