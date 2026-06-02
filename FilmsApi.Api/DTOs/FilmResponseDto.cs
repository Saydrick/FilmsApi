namespace FilmsApi.Api.DTOs
{
    /// <summary>
    /// Données retournées par l'API pour un film.
    /// </summary>
    public class FilmResponseDto
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public int Annee { get; set; }
        public string? Synopsis { get; set; }
        public string? Realisateur { get; set; }
        public string? Duree { get; set; }
        public List<string> Genres { get; set; } = new();
        public double? Note { get; set; }
        public string Statut { get; set; } = string.Empty;
        public string? AfficheUrl { get; set; }
        public bool EstRecent { get; set; }
    }
}