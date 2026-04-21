using FilmsApi.Api.Exceptions;
using FilmsApi.Api.Models;
using FilmsApi.Api.Repositories;

namespace FilmsApi.Api.Services
{
    /// <summary>
    /// Service générique fournissant les opérations de base communes à tous les médias.
    /// FilmService et SerieService héritent de cette classe pour partager cette logique.
    /// </summary>
    /// <typeparam name="T">Le type du média géré (Film, Serie, etc.).</typeparam>
    // T doit hériter de Media pour garantir l'accès aux propriétés communes (Id, Titre, etc.)
    public class MediaService<T> where T : Media
    {
        protected readonly IRepository<T> _repository;

        public MediaService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public IEnumerable<T> GetAll() => _repository.GetAll();

        public T GetById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new NotFoundException(typeof(T).Name, id);
            return entity;
        }

        public void Delete(int id)
        {
            GetById(id);
            _repository.Delete(id);
        }

        public IEnumerable<T> GetFiltered(Func<T, bool> filtre) =>
            _repository.GetAll().Where(filtre);
    }
}