using System.ComponentModel.DataAnnotations.Schema;

namespace MovieVote.Db;

public class Vote
{
    public int Id { get; set; }
    public User Voter { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
    
    [ForeignKey("Voter")]
    public int VoterId { get; set; }
    
    [ForeignKey("Movie")]
    public int MovieId { get; set; }
}