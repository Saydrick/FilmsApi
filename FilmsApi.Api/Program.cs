using FilmsApi.Api.Models;
using FilmsApi.Api.Repositories;
using FilmsApi.Api.Services;
using FilmsApi.Api.Options;
using FilmsApi.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Base de données EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Enregistrement des repositories
builder.Services.AddScoped<IRepository<Film>, FilmEfRepository>();
builder.Services.AddScoped<IRepository<Serie>, SerieEfRepository>();

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