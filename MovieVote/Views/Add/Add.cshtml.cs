using MovieVote.Db;

namespace MovieVote.Views.Add;

public class AddModel
{
    public (bool Success, string Reason)? Result;
    public int? Id;
    public Movie? Movie;

    public AddModel()
    {
    }

    public AddModel((bool Success, string Reason)? result)
    {
        Result = result;
    }
}