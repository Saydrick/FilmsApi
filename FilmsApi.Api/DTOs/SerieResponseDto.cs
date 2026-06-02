namespace FilmsApi.Api.DTOs
{
    public class SerieResponseDto
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public int? AnneeDebut { get; set; }
        public int? AnneeFin { get; set; }
        public int? NbEpisode { get; set; }
        public int? NbSaison { get; set; }
        public int? DureeMinute { get; set; }
        public List<string> Genres { get; set; } = new();
        public double? Note { get; set; }
        public bool EnCours { get; set; }
        public string Statut { get; set; } = "A voir";
        public bool EstRecent { get; set; }
    }
}