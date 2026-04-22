using FilmsApi.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configuration TMDB
var tmdbApiKey = builder.Configuration["Tmdb:ApiKey"];
// Console.WriteLine($"Clé TMDB : {tmdbApiKey?[..10]}..."); // affiche les 10 premiers caractères
builder.Services.AddHttpClient<TmdbService>(client =>
{
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tmdbApiKey);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

// --- Test temporaire --------------------------------------------------------
var tmdbService = app.Services.GetRequiredService<TmdbService>();
var repoFilm = new FilmsApi.Api.Repositories.FilmRepository();
var filmService = new FilmsApi.Api.Services.FilmService(repoFilm);

repoFilm.Add(new FilmsApi.Api.Models.Film
{
    Titre = "Inception",
    Annee = 2010,
    Realisateur = "Christopher Nolan"
});

var resultats = await tmdbService.SearchFilmsAsync("Inception");
Console.WriteLine("---- Film -> Inception -------------------------------------");
foreach (var r in resultats.Take(3))
    Console.WriteLine($"{r.Titre} ({r.ReleaseDate}) - {r.Id}");

var film = filmService.GetById(1);
await tmdbService.EnrichirFilmAsync(film, 27205);
Console.WriteLine("---- Film -> Description -----------------------------------");
Console.WriteLine(film.GetDescription());
Console.WriteLine("---- Film -> Affiche ---------------------------------------");
Console.WriteLine($"Affiche : {film.AfficheUrl}");
// -----------------------------------------------------------------------------


app.Run();