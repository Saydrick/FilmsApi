using FilmsApi.Api.Models;

namespace FilmsApi.Api.Repositories
{
    /// <summary>
    /// Repository en mémoire pour les films.
    /// Remplacé plus tard par EF Core.
    /// </summary>
    public class FilmRepository : IRepository<Film>
    {
        private readonly List<Film> _films = new();
        /// <summary>
        /// Compteur auto-incrémenté pour la génération des identifiants.
        /// Ne se base pas sur Count pour éviter les collisions après suppression.
        /// </summary>
        private int _nextId = 1;

        public IEnumerable<Film> GetAll() => _films;

        public Film? GetById(int id) =>
            _films.FirstOrDefault(f => f.Id == id);

        public void Add(Film film)
        {
            film.Id = _nextId++;
            _films.Add(film);
        }

        public void Update(Film film)
        {
            var index = _films.FindIndex(f => f.Id == film.Id);
            if (index >= 0) _films[index] = film;
        }

        public void Delete(int id) =>
            _films.RemoveAll(f => f.Id == id);


        /// <summary>
        /// Recherche des films par titre ou réalisateur.
        /// </summary>
        /// <param name="terme">Terme de recherche (insensible à la casse).</param>
        public IEnumerable<Film> Search(string terme) =>
            _films
                .Where(f => f.Titre.Contains(terme, StringComparison.OrdinalIgnoreCase) || f.Realisateur != null && f.Realisateur.Contains(terme, StringComparison.OrdinalIgnoreCase))
                .OrderBy(f => f.Titre);

        public IEnumerable<Film> GetByStatus(string statut) =>
            _films
                .Where(f => f.Statut == statut)
                .OrderBy(f => f.Titre);

        public IEnumerable<Film> GetByGenre(string genre) =>
            _films
                .Where(f => f.Genres.Any(g => g.Equals(genre, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(f => f.Titre);

        /// <summary>
        /// Retourne les films des 3 dernières années, triés du plus récent au plus ancien.
        /// </summary>
        public IEnumerable<Film> GetRecents() =>
            _films
                .Where(f => f.EstRecent())
                .OrderByDescending(f => f.Annee)
                .ThenBy(f => f.Titre);

        public IEnumerable<Film> GetTopNotes(int n) =>
            _films
                .Where(f => f.Note != null)
                .OrderByDescending(f => f.Note)
                .Take(n);

    }
}