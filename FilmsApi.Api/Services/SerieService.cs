using FilmsApi.Api.Exceptions;
using FilmsApi.Api.Models;
using FilmsApi.Api.Repositories;

namespace FilmsApi.Api.Services
{
    /// <summary>
    /// Service gérant la logique métier spécifique aux série.
    /// Hérite de MediaService pour les opérations communes (GetAll, GetById, Delete).
    /// </summary>
    public class SerieService : MediaService<Serie>
    {
        public SerieService(IRepository<Serie> repository) : base(repository) { }

        public Serie Add(Serie serie)
        {
            Validate(serie);
            _repository.Add(serie);
            return serie;
        }

        public Serie Update(Serie serie)
        {
            GetById(serie.Id);
            Validate(serie);
            _repository.Update(serie);
            return serie;
        }

        /// <summary>
        /// Valide les données d'une série avant ajout ou mise à jour.
        /// </summary>
        /// <param name="serie">La série à valider.</param>
        /// <exception cref="ValidationException">Lancée si une ou plusieurs règles ne sont pas respectées.</exception>
        private void Validate(Serie serie)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(serie.Titre))
                errors.Add("Le titre est obligatoire.");

            if (serie.AnneeDebut.HasValue && serie.AnneeDebut < 1900)
                errors.Add("L'année de début doit être supérieure à 1900.");

            if (serie.AnneeFin.HasValue && serie.AnneeDebut.HasValue
                && serie.AnneeFin < serie.AnneeDebut)
                errors.Add("L'année de fin ne peut pas être antérieure à l'année de début.");

            if (serie.Note.HasValue && (serie.Note < 0 || serie.Note > 100))
                errors.Add("La note doit être comprise entre 0 et 100.");

            if (errors.Any())
                throw new ValidationException(errors);
        }
    }
}