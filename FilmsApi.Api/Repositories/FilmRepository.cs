using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}