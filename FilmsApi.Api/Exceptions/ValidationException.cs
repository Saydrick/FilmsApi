namespace FilmsApi.Api.Exceptions
{
    /// <summary>
    /// Exception levée lorsque des règles de validation métier ne sont pas respectées.
    /// Contient la liste détaillée des erreurs de validation.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Liste détaillée des erreurs de validation.
        /// </summary>
        public IEnumerable<string> Errors { get; }

        public ValidationException(string message) : base(message)
        {
            Errors = new List<string> { message };
        }

        public ValidationException(IEnumerable<string> errors)
            : base("Des erreurs de validation sont survenues.")
        {
            Errors = errors;
        }
    }
}