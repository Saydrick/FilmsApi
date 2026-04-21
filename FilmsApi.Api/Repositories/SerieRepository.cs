using FilmsApi.Api.Models;

namespace FilmsApi.Api.Repositories
{
    /// <summary>
    /// Repository en mémoire pour les séries.
    /// Remplacé plus tard par EF Core.
    /// </summary>
    public class SerieRepository : IRepository<Serie>
    {
        private readonly List<Serie> _series = new();
        /// <summary>
        /// Compteur auto-incrémenté pour la génération des identifiants.
        /// Ne se base pas sur Count pour éviter les collisions après suppression.
        /// </summary>
        private int _nextId = 1;

        public IEnumerable<Serie> GetAll() => _series;

        public Serie? GetById(int id) =>
            _series.FirstOrDefault(s => s.Id == id);

        public void Add(Serie serie)
        {
            serie.Id = _nextId++;
            _series.Add(serie);
        }

        public void Update(Serie serie)
        {
            var index = _series.FindIndex(s => s.Id == serie.Id);
            if (index >= 0) _series[index] = serie;
        }

        public void Delete(int id) =>
            _series.RemoveAll(s => s.Id == id);


        public IEnumerable<Serie> GetLongSeries(int nbSaison) =>
            _series
                .Where(s => s.NbSaison >= nbSaison)
                .OrderBy(s => s.Titre);

        public IEnumerable<Serie> GetUnfinished() =>
            _series
                .Where(s => s.EnCours)
                .OrderBy(s => s.Titre);
    }
}