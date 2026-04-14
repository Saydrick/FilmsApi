using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Api.Models
{
    public abstract class Media
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public double? Note { get; set; }
        public List<string> Genres { get; set; } = new();
        public string Statut { get; set; } = "A voir";

        public abstract string GetDescription();

    }


}