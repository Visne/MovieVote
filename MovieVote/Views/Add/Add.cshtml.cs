using MovieVote.Views.Shared;

namespace MovieVote.Views.Add;

public class AddModel
{
    public (bool Success, string Reason)? Result;
    public string? Id;
    public MovieModel? Movie;
}