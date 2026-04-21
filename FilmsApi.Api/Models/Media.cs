namespace FilmsApi.Api.Models
{
    /// <summary>
    /// Classe abstraite commune à tous les types de médias (Film, Serie).
    /// </summary>
    public abstract class Media
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        /// <summary>
        /// Note sur 100.
        /// </summary>
        public double? Note { get; set; }
        public List<string> Genres { get; set; } = new();
        /// <summary>
        /// Statut de visionnage. Valeurs possibles : "A voir", "Vu", "En cours".
        /// </summary>
        public string Statut { get; set; } = "A voir";

        public abstract string GetDescription();

    }


}