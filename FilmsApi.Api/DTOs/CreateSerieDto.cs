namespace FilmsApi.Api.DTOs;

public class CreateSerieDto
{
    public string Titre { get; set; } = string.Empty;
    public string? Synopsis { get; set; }
    public int AnneeDebut { get; set; }
    public int AnneeFin { get; set; }
    public int? NbEpisode { get; set; }
    public int? NbSaison { get; set; }
    public List<string> Genres { get; set; } = new();
    public double? Note { get; set; }
    public bool EnCours { get; set; }
    public string Statut { get; set; } = "A voir";
}
