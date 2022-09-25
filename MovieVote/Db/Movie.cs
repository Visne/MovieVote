using System.ComponentModel.DataAnnotations.Schema;

namespace MovieVote.Db;

public class Movie
{
    public int Id { get; set; }
    public int TmdbId { get; set; }
    public string? ImdbId { get; set; }
    public string Title { get; set; } = null!;
    public DateOnly? ReleaseDate { get; set; }
    public string? Poster { get; set; }
    public string? Overview { get; set; }
    public string? Tagline { get; set; }
    public DateTime AddedOn { get; set; }
    public User? AddedBy { get; set; }
    
    [ForeignKey("AddedBy")]
    public int? AddedById { get; set; }
}