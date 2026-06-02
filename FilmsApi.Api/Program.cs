using FilmsApi.Api.Models;
using FilmsApi.Api.Repositories;
using FilmsApi.Api.Services;
using FilmsApi.Api.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Enregistrement des repositories
builder.Services.AddSingleton<IRepository<Film>, FilmRepository>();
builder.Services.AddSingleton<IRepository<Serie>, SerieRepository>();

// Enregistrement des services
builder.Services.AddScoped<FilmService>();
builder.Services.AddScoped<SerieService>();

// Configuration TMDB
builder.Services.Configure<TmdbOptions>(
    builder.Configuration.GetSection("Tmdb")
);

builder.Services.AddHttpClient<TmdbService>(client =>
{
    var apiKey = builder.Configuration["Tmdb:ApiKey"];
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
});

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();