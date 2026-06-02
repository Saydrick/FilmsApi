using System.Text.Json.Serialization;

namespace FilmsApi.Api.Models
{
    // ── Recherche films ─────────────────────────────────────────────
    public class TmdbSearchResponse
    {
        [JsonPropertyName("results")]
        public List<TmdbFilmResult> Results { get; set; } = new();
    }

    public class TmdbFilmResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Titre { get; set; } = string.Empty;

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }
    }

    public class TmdbFilmResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Titre { get; set; } = string.Empty;

        [JsonPropertyName("overview")]
        public string Overview { get; set; } = string.Empty;

        [JsonPropertyName("runtime")]
        public int? Runtime { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        [JsonPropertyName("genres")]
        public List<TmdbGenre> Genres { get; set; } = new();
    }

    // ── Recherche séries ─────────────────────────────────────────────
    public class TmdbSerieSearchResponse
    {
        [JsonPropertyName("results")]
        public List<TmdbSerieResult> Results { get; set; } = new();
    }

    public class TmdbSerieResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; } = string.Empty;

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }
    }

    public class TmdbSerieResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("overview")]
        public string Overview { get; set; } = string.Empty;

        [JsonPropertyName("number_of_seasons")]
        public int? NumberOfSeasons { get; set; }

        [JsonPropertyName("number_of_episodes")]
        public int? NumberOfEpisodes { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; } = string.Empty;

        [JsonPropertyName("last_air_date")]
        public string LastAirDate { get; set; } = string.Empty;

        [JsonPropertyName("in_production")]
        public bool InProduction { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        [JsonPropertyName("genres")]
        public List<TmdbGenre> Genres { get; set; } = new();
    }

    // ── Commun ─────────────────────────────────────────────
    public class TmdbGenre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}