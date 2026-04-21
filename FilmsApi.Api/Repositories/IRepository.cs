namespace FilmsApi.Api.Repositories
{
    /// <summary>
    /// Contrat générique définissant les opérations CRUD de base.
    /// Toute classe d'accès aux données doit implémenter cette interface.
    /// </summary>
    /// <typeparam name="T">Le type de l'entité gérée par le répository</typeparam>
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}