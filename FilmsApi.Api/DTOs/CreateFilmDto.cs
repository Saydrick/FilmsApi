namespace FilmsApi.Api.DTOs
{
    /// <summary>
    /// Données reçues lors de la création d'un film.
    /// </summary>
    public class CreateFilmDto
    {
        public string Titre { get; set; } = string.Empty;
        public int Annee { get; set; }
        public string? Synopsis { get; set; }
        public string? Realisateur { get; set; }
        public int? DureeMinute { get; set; }
        public List<string> Genres { get; set; } = new();
        public double? Note { get; set; }
        public string Statut { get; set; } = "A voir";
    }
}