using FilmsApi.Api.Models;

namespace FilmsApi.Api.Repositories
{
    public class FilmRepository : IRepository<Film>
    {
        private readonly List<Film> _films = new();

        public IEnumerable<Film> GetAll() => _films;

        public Film? GetById(int id) =>
            _films.FirstOrDefault(f => f.Id == id);

        public void Add(Film film)
        {
            film.Id = _films.Count + 1;
            _films.Add(film);
        }

        public void Update(Film film)
        {
            var index = _films.FindIndex(f => f.Id == film.Id);
            if (index >= 0) _films[index] = film;
        }

        public void Delete(int id) =>
            _films.RemoveAll(f => f.Id == id);


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