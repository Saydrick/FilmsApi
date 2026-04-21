namespace FilmsApi.Api.Exceptions
{
    /// <summary>
    /// Exception levée lorsqu'une entité recherchée est introuvable.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string entityName, int id)
            : base($"{entityName} avec l'id {id} est introuvable.") { }
    }
}