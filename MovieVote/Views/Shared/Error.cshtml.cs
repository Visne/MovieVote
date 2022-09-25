namespace MovieVote.Views.Shared;

public class ErrorModel
{
    public ErrorModel(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public ErrorModel()
    {
    }
    
    public string? ErrorMessage;
}
