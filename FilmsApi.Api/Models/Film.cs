namespace FilmsApi.Api.Models
{
    public class Film : Media
    {
        public int? DureeMinute { get; set; }
        public string? Realisateur { get; set; }
        public int Annee { get; set; }
        public int? TmdbId { get; set; }
        public string? AfficheUrl { get; set; }

        public override string GetDescription()
        {
            string duree = DureeMinute.HasValue
            ? $"{TimeSpan.FromMinutes(DureeMinute.Value).Hours}h{TimeSpan.FromMinutes(DureeMinute.Value).Minutes:D2}"
            : "durée inconnue";

            string genres = Genres.Any() ? string.Join(", ", Genres) : "Aucun genre";

            return $"{Titre} ({Annee}) - Film réalisé par {Realisateur}, {duree}" + "\n" + $"{Note} - {genres}" + "\n";
        }
        public bool EstRecent() => Annee >= DateTime.Now.Year - 3;
    }
}