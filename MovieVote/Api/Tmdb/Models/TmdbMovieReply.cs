using Newtonsoft.Json;

namespace MovieVote.Api.Tmdb.Models;

public class TmdbMovieReply
{
    [JsonProperty("adult")]
    public bool? Adult;

    [JsonProperty("backdrop_path")]
    public string? BackdropPath;

    [JsonProperty("belongs_to_collection")]
    public dynamic? BelongsToCollection;

    [JsonProperty("budget")]
    public int? Budget;

    [JsonProperty("genres")]
    public List<TmdbGenre>? Genres;

    [JsonProperty("homepage")]
    public string? Homepage;

    [JsonProperty("id")]
    public int? Id;

    [JsonProperty("imdb_id")]
    public string? ImdbId;

    [JsonProperty("original_language")]
    public string? OriginalLanguage;

    [JsonProperty("original_title")]
    public string? OriginalTitle;

    [JsonProperty("overview")]
    public string? Overview;

    [JsonProperty("popularity")]
    public double? Popularity;

    [JsonProperty("poster_path")]
    public object? PosterPath;

    [JsonProperty("production_companies")]
    public List<TmdbProductionCompany>? ProductionCompanies;

    [JsonProperty("production_countries")]
    public List<TmdbProductionCountry>? ProductionCountries;

    [JsonProperty("release_date")]
    public string? ReleaseDate;

    [JsonProperty("revenue")]
    public int? Revenue;

    [JsonProperty("runtime")]
    public int? Runtime;

    [JsonProperty("spoken_languages")]
    public List<TmdbSpokenLanguage>? SpokenLanguages;

    [JsonProperty("status")]
    public string? Status;

    [JsonProperty("tagline")]
    public string? Tagline;

    [JsonProperty("title")]
    public string? Title;

    [JsonProperty("video")]
    public bool? Video;

    [JsonProperty("vote_average")]
    public double? VoteAverage;

    [JsonProperty("vote_count")]
    public int? VoteCount;
}