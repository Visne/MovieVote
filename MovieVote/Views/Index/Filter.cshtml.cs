namespace MovieVote.Views.Index;

public struct FilterModel
{
    public FilterModel(string text, string content)
    {
        Text = text;
        Content = content;
    }
    
    public string Text;
    public string Content;
}