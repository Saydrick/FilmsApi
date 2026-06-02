using System.Text.Json;
using FilmsApi.Api.Models;
using FilmsApi.Api.Options;
using Microsoft.Extensions.Options;

namespace FilmsApi.Api.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly TmdbOptions _options;

        public TmdbService(HttpClient httpClient, IOptions<TmdbOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        /// <summary>
        /// Recherche des films sur TMDB par titre
        /// </summary>
        /// <param name="titre">Titre du film à rechercher.</param>
        /// <returns>Liste des résultats TMDB correspondants.</returns>
        public async Task<List<TmdbFilmResult>> SearchFilmsAsync(string titre)
        {
            var url = $"{_options.BaseUrl}/search/movie?query={Uri.EscapeDataString(titre)}&language=fr-FR";
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
            var url = $"{_options.BaseUrl}/movie/{tmdbId}?language=fr-FR";
            var response = await _httpClient.GetStringAsync(url);

            var tmdbFilm = JsonSerializer.Deserialize<TmdbFilmResponse>(response);

            if (tmdbFilm == null) return;

            film.TmdbId = tmdbFilm.Id;
            film.Synopsis = tmdbFilm.Overview;
            // TMDB note sur 10, on convertit sur 100 pour rester cohérent avec notre modèle
            film.Note = tmdbFilm.VoteAverage * 10;
            film.DureeMinute = tmdbFilm.Runtime;
            film.AfficheUrl = $"{_options.ImageBaseUrl}{tmdbFilm.PosterPath}";
            film.Genres = tmdbFilm.Genres.Select(g => g.Name).ToList();
        }

        /// <summary>
        /// Enrichit une serie local avec les données TMDB.
        /// Met à jour le synopsis
        /// </summary>
        /// <param name="serie"></param>
        /// <returns></returns>
        public async Task EnrichirSerieAsync(Serie serie, int tmdbId)
        {
            var url = $"{_options.BaseUrl}/movie/{tmdbId}?language=fr-FR";
            var response = await _httpClient.GetStringAsync(url);

            var tmdbSerie = JsonSerializer.Deserialize<TmdbSerieResponse>(response);

            if (tmdbSerie == null) return;

            serie.Synopsis = tmdbSerie.Overview;
            serie.Note = tmdbSerie.VoteAverage * 10;
            serie.AfficheUrl = tmdbSerie.PosterPath != null
                ? $"{_options.ImageBaseUrl}{tmdbSerie.PosterPath}"
                : null;
            serie.Genres = tmdbSerie.Genres.Select(g => g.Name).ToList();
            serie.NbSaison = tmdbSerie.NumberOfSeasons;
            serie.NbEpisode = tmdbSerie.NumberOfEpisodes;
            serie.EnCours = tmdbSerie.InProduction;

            if (DateTime.TryParse(tmdbSerie.FirstAirDate, out var dateDebut))
                serie.AnneeDebut = dateDebut.Year;

            if (DateTime.TryParse(tmdbSerie.LastAirDate, out var dateFin))
                serie.AnneeFin = dateFin.Year;
        }
    }
}