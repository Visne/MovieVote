namespace MovieVote.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Returns value if not null or empty, otherwise returns alt.
    /// </summary>
    /// <param name="value">Value to return if it is not null or empty</param>
    /// <param name="alt">Alternative value to return if value is null or empty</param>
    public static string Or(this string? value, string alt)
    {
        if (string.IsNullOrEmpty(value))
        {
            return alt;
        }

        return value;
    }
}