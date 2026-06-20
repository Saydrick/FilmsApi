using Microsoft.EntityFrameworkCore;
using FilmsApi.Api.Models;

namespace FilmsApi.Api.Data;

/// <summary>
/// Contexte de base de données — gère la connexion et les entités.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Film> Films { get; set; }
    public DbSet<Serie> Series { get; set; }
}
