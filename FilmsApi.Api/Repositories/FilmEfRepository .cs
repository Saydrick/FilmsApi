// Repositories/FilmEfRepository.cs
using Microsoft.EntityFrameworkCore;
using FilmsApi.Api.Data;
using FilmsApi.Api.Models;

namespace FilmsApi.Api.Repositories;

/// <summary>
/// Repository EF Core pour les films — remplace FilmRepository en mémoire.
/// </summary>
public class FilmEfRepository : IRepository<Film>
{
    private readonly AppDbContext _context;

    public FilmEfRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Film> GetAll() => _context.Films.ToList();

    public Film? GetById(int id) =>
        _context.Films.FirstOrDefault(f => f.Id == id);

    public void Add(Film film)
    {
        _context.Films.Add(film);
        _context.SaveChanges();
    }

    public void Update(Film film)
    {
        _context.Films.Update(film);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var film = GetById(id);
        if (film != null)
        {
            _context.Films.Remove(film);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Film> Search(string terme) =>
        _context.Films
            .Where(f => f.Titre.Contains(terme) ||
                       (f.Realisateur != null && f.Realisateur.Contains(terme)))
            .OrderBy(f => f.Titre)
            .ToList();

    public IEnumerable<Film> GetByStatus(string statut) =>
        _context.Films
            .Where(f => f.Statut == statut)
            .OrderBy(f => f.Titre)
            .ToList();

    public IEnumerable<Film> GetTopNotes(int n) =>
        _context.Films
            .Where(f => f.Note != null)
            .OrderByDescending(f => f.Note)
            .Take(n)
            .ToList();
}