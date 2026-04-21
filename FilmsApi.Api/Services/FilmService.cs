using FilmsApi.Api.Exceptions;
using FilmsApi.Api.Models;
using FilmsApi.Api.Repositories;

namespace FilmsApi.Api.Services
{
    /// <summary>
    /// Service gérant la logique métier spécifique aux films.
    /// Hérite de MediaService pour les opérations communes (GetAll, GetById, Delete).
    /// </summary>
    public class FilmService : MediaService<Film>
    {
        public FilmService(IRepository<Film> repository) : base(repository) { }

        public Film Add(Film film)
        {
            Validate(film);
            _repository.Add(film);
            return film;
        }

        public Film Update(Film film)
        {
            GetById(film.Id);
            Validate(film);
            _repository.Update(film);
            return film;
        }

        /// <summary>
        /// Valide les données d'un film avant ajout ou mise à jour.
        /// </summary>
        /// <param name="film">Le film à valider.</param>
        /// <exception cref="ValidationException">Lancée si une ou plusieurs règles ne sont pas respectées.</exception>
        private void Validate(Film film)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(film.Titre))
                errors.Add("Le titre est obligatoire.");

            // 1888 = date du 1er film de l'histoire
            if (film.Annee < 1888 || film.Annee > DateTime.Now.Year + 5)
                errors.Add($"L'année doit être comprise entre 1888 et {DateTime.Now.Year + 5}.");

            if (film.Note.HasValue && (film.Note < 0 || film.Note > 100))
                errors.Add("La note doit être comprise entre 0 et 100.");

            if (errors.Any())
                throw new ValidationException(errors);
        }
    }
}