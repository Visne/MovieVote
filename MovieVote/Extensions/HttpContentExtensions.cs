using Newtonsoft.Json;

namespace MovieVote.Extensions;

public static class HttpContentExtensions
{


    public static async Task<T?> Deserialize<T>(this JsonSerializer serializer, HttpContent content)
    {
        var reader = new JsonTextReader(new StringReader(await content.ReadAsStringAsync()));
        return serializer.Deserialize<T>(reader);
    }
}