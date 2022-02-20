namespace MovieVote.Extensions;

public static class IntExtensions
{
    /// <summary>
    ///     Return value as string if it is not null or 0, otherwise return alternative string.
    /// </summary>
    /// <param name="value">Value to check and possibly return as string</param>
    /// <param name="alt">String that will be returned if value is null or 0</param>
    public static string Or(this int? value, string alt)
    {
        if (value != null && value != 0)
        {
            return value.ToString() ?? alt;
        }

        return alt;
    }
}