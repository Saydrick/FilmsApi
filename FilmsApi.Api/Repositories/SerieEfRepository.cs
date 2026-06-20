using Microsoft.EntityFrameworkCore;
using FilmsApi.Api.Data;
using FilmsApi.Api.Models;


namespace FilmsApi.Api.Repositories;

/// <summary>
/// Repository EF Core pour les series — remplace SerieRepository en mémoire.
/// </summary>
public class SerieEfRepository : IRepository<Serie>
{
    private readonly AppDbContext _context;

    public SerieEfRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Serie> GetAll() => _context.Series.ToList();

    public Serie? GetById(int id) =>
        _context.Series.FirstOrDefault(s => s.Id == id);

    public void Add(Serie serie)
    {
        _context.Series.Add(serie);
        _context.SaveChanges();
    }

    public void Update(Serie serie)
    {
        _context.Series.Update(serie);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var serie = GetById(id);
        if (serie != null)
        {
            _context.Series.Remove(serie);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Serie> Search(string terme) =>
        _context.Series
            .Where(s => s.Titre.Contains(terme))
            .OrderBy(s => s.Titre)
            .ToList();

    public IEnumerable<Serie> GetByStatus(string statut) =>
        _context.Series
            .Where(s => s.Statut == statut)
            .OrderBy(s => s.Titre)
            .ToList();

    public IEnumerable<Serie> GetTopNotes(int n) =>
    _context.Series
        .Where(s => s.Note != null)
        .OrderByDescending(s => s.Note)
        .Take(n)
        .ToList();
}
