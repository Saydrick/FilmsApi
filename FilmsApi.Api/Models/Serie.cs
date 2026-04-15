namespace FilmsApi.Api.Models
{
    public class Serie : Media
    {
        public int? NbEpisode { get; set; }
        public int? NbSaison { get; set; }
        public int? AnneeDebut { get; set; }
        public int? AnneeFin { get; set; }
        public bool EnCours { get; set; }

        public override string GetDescription()
        {

            string genres = Genres.Any() ? string.Join(", ", Genres) : "Aucun genre";

            return $"{Titre} ({AnneeDebut}-{AnneeFin}) - {NbSaison} saisons pour un total de {NbEpisode} épisodes ({(EnCours ? "En cours" : "Terminée")})" + "\n" + $"{Note} - {genres}" + "\n";
        }
        public bool EstRecent() => AnneeFin >= DateTime.Now.Year - 3;


    }
}