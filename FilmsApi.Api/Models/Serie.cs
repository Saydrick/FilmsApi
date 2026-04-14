using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Api.Models
{
    public class Serie : Media
    {
        public int? NbEpisode { get; set; }
        public int? NbSaison { get; set; }
        public int? AnneeDebut { get; set; }
        public int? AnneeFin { get; set; }
        public bool EnCours { get; set; }

        public override string GetDescription() =>
            $"{Titre} ({AnneeDebut}-{AnneeFin}) - {NbSaison} saisons pour un total de {NbEpisode} épisodes ({(EnCours ? "En cours" : "Terminée")})";
        public bool EstRecent() => AnneeFin >= DateTime.Now.Year - 3;


    }
}