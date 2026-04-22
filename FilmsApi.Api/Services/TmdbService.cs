using System.Text.Json;
using FilmsApi.Api.Models;

namespace FilmsApi.Api.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.themoviedb.org/3";
        private const string ImageBaseUrl = "https://image.tmdb.org/t/p/w500";

        public TmdbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Recherche des films sur TMDB par titre
        /// </summary>
        /// <param name="titre">Titre du film à rechercher.</param>
        /// <returns>Liste des résultats TMDB correspondants.</returns>
        public async Task<List<TmdbFilmResult>> SearchFilmsAsync(string titre)
        {
            var url = $"{BaseUrl}/search/movie?query={Uri.EscapeDataString(titre)}&language=fr-FR";
            var content = await _httpClient.GetStringAsync(url);

            var result = JsonSerializer.Deserialize<TmdbSearchResponse>(content);
            return result?.Results ?? new List<TmdbFilmResult>();
        }

        /// <summary>
        /// Enrichit un film local avec les données TMDB.
        /// Met à jour le synopsis
        /// </summary>
        /// <param name="titre"></param>
        /// <returns></returns>
        public async Task EnrichirFilmAsync(Film film, int tmdbId)
        {
            var url = $"{BaseUrl}/movie/{tmdbId}?language=fr-FR";
            var response = await _httpClient.GetStringAsync(url);

            var tmdbFilm = JsonSerializer.Deserialize<TmdbFilmResponse>(response);

            if (tmdbFilm == null) return;

            film.TmdbId = tmdbFilm.Id;
            film.Synopsis = tmdbFilm.Overview;
            // TMDB note sur 10, on convertit sur 100 pour rester cohérent avec notre modèle
            film.Note = tmdbFilm.VoteAverage * 10;
            film.DureeMinute = tmdbFilm.Runtime;
            film.AfficheUrl = tmdbFilm.PosterPath != null
                ? $"{ImageBaseUrl}{tmdbFilm.PosterPath}"
                : null;
            film.Genres = tmdbFilm.Genres.Select(g => g.Name).ToList();
        }
    }
}