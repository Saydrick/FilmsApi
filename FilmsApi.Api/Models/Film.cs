using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Api.Models
{
    public class Film : Media
    {
        public int? DureeMinute { get; set; }
        public string? Realisateur { get; set; }
        public int Annee { get; set; }
        public int? TmdbId { get; set; }
        public string? AfficheUrl { get; set; }

        public override string GetDescription() =>
            $"{Titre} ({Annee}) - Film réalisé par {Realisateur}, {DureeMinute} min";
        public bool EstRecent() => Annee >= DateTime.Now.Year - 3;

    }
}