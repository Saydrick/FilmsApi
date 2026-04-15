using FilmsApi.Api.Models;

namespace FilmsApi.Api.Repositories
{
    public class SerieRepository : IRepository<Serie>
    {
        private readonly List<Serie> _series = new();

        public IEnumerable<Serie> GetAll() => _series;

        public Serie? GetById(int id) =>
            _series.FirstOrDefault(s => s.Id == id);

        public void Add(Serie serie)
        {
            serie.Id = _series.Count + 1;
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
                .Where(s => s.EnCours == true)
                .OrderBy(s => s.Titre);
    }
}