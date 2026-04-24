using FilmsApi.Api.Models;
using FilmsApi.Api.Repositories;
using FilmsApi.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Enregistrement des repositories
builder.Services.AddSingleton<IRepository<Film>, FilmRepository>();
builder.Services.AddSingleton<IRepository<Serie>, SerieRepository>();

// Enregistrement des services
builder.Services.AddScoped<FilmService>();
builder.Services.AddScoped<SerieService>();

// Configuration TMDB
var tmdbApiKey = builder.Configuration["Tmdb:ApiKey"];
builder.Services.AddHttpClient<TmdbService>(client =>
{
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tmdbApiKey);
});

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();