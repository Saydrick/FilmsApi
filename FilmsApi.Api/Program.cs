using FilmsApi.Api.Models;
using FilmsApi.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

// Test temporaire
var repoFilm = new FilmRepository();
var repoSerie = new SerieRepository();

repoFilm.Add(new Film
{
    Titre = "La cité de la peur",
    Annee = 1994,
    Realisateur = "Alain Berberian",
    DureeMinute = 93,
    Genres = { "Comédie" },
    Note = 75,
    Statut = "Vu"
});

repoFilm.Add(new Film
{
    Titre = "Avatar 3",
    Annee = 2025,
    Realisateur = "James Cameron",
    DureeMinute = 198,
    Genres = { "Science-Fiction", "Aventure", "Fatastique" },
    Note = 74
});

repoFilm.Add(new Film
{
    Titre = "Super Mario Bros., le film",
    Annee = 2023,
    Realisateur = "Aaron Horvath",
    DureeMinute = 92,
    Genres = { "Familial", "Comédie", "Aventure", "Fantastique", "Animation" },
    Note = 76
});

repoFilm.Add(new Film
{
    Titre = "War Machine",
    Annee = 2026,
    Realisateur = "Patrick Hughes",
    DureeMinute = 110,
    Genres = { "Action", "Science-Fiction", "Thriller" },
    Note = 72
});

repoFilm.Add(new Film
{
    Titre = "Demon Slayer : Kimetsu no Yaiba - Le film : La Forteresse infinie",
    Annee = 2025,
    Realisateur = "Haruo Sotozaki",
    DureeMinute = 155,
    Genres = { "Animation", "Action", "Fantastique" },
    Note = 77,
    Statut = "Vu"
});


repoFilm.Add(new Film
{
    Titre = "Le Diable s'habille en Prada",
    Annee = 2006,
    Realisateur = "David Frankel",
    DureeMinute = 110,
    Genres = { "Drame", "Comédie" },
    Note = 74,
    Statut = "Vu"
});


repoSerie.Add(new Serie
{
    Titre = "Brooklyn 99",
    AnneeDebut = 2013,
    AnneeFin = 2021,
    NbEpisode = 153,
    NbSaison = 8,
    Genres = { "Crime", "Comédie" },
    Note = 82,
    Statut = "Vu",
    EnCours = true // Test : à supprimer
});

repoSerie.Add(new Serie
{
    Titre = "The good place",
    AnneeDebut = 2016,
    AnneeFin = 2020,
    NbEpisode = 53,
    NbSaison = 4,
    Genres = { "Science-Fiction", "Fantastique", "Comédie" },
    Note = 80,
    Statut = "Vu"
});

repoSerie.Add(new Serie
{
    Titre = "Mentalist",
    AnneeDebut = 2008,
    AnneeFin = 2014,
    NbEpisode = 151,
    NbSaison = 7,
    Genres = { "Crime", "Drame", "Mystère" },
    Note = 84,
    Statut = "Vu"
});

repoSerie.Add(new Serie
{
    Titre = "Peaky Blinders",
    AnneeDebut = 2013,
    AnneeFin = 2022,
    NbEpisode = 36,
    NbSaison = 6,
    Genres = { "Crime", "Drame" },
    Note = 85
});


Console.WriteLine("----- Search -----");
foreach (var film in repoFilm.Search("film"))
{
    Console.WriteLine(film.GetDescription());
}

Console.WriteLine("----- GetByStatus -----");
foreach (var film in repoFilm.GetByStatus("Vu"))
{
    Console.WriteLine(film.GetDescription());
}

Console.WriteLine("----- GetByGenre -----");
foreach (var film in repoFilm.GetByGenre("Science-fiction"))
{
    Console.WriteLine(film.GetDescription());
}

Console.WriteLine("----- GetRecent -----");
foreach (var film in repoFilm.GetRecents())
{
    Console.WriteLine(film.GetDescription());
}

Console.WriteLine("----- GetTopNotes -----");
foreach (var film in repoFilm.GetTopNotes(5))
{
    Console.WriteLine(film.GetDescription());
}

Console.WriteLine("----- GetLongSeries -----");
foreach (var serie in repoSerie.GetLongSeries(4))
{
    Console.WriteLine(serie.GetDescription());
}

Console.WriteLine("----- GetUnfinished -----");
foreach (var serie in repoSerie.GetUnfinished())
{
    Console.WriteLine(serie.GetDescription());
}


/*
foreach (var film in repoFilm.GetAll())
{
    Console.WriteLine(film.GetDescription());
}

foreach (var serie in repoSerie.GetAll())
{
    Console.WriteLine(serie.GetDescription());
}
*/


app.Run();