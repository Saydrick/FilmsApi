using FilmsApi.Api.DTOs;
using FilmsApi.Api.Exceptions;
using FilmsApi.Api.Models;
using FilmsApi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmsApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmsController : ControllerBase
{
    private readonly FilmService _filmService;
    private readonly TmdbService _tmdbService;

    public FilmsController(FilmService filmService, TmdbService tmdbService)
    {
        _filmService = filmService;
        _tmdbService = tmdbService;
    }

    // GET api/films
    [HttpGet]
    public IActionResult GetAll()
    {
        var films = _filmService.GetAll()
            .Select(f => _filmService.ToDto(f));
        return Ok(films);
    }

    // GET api/films/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var film = _filmService.GetById(id);
            return Ok(_filmService.ToDto(film));
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }

    // POST api/films
    [HttpPost]
    public IActionResult Add([FromBody] CreateFilmDto dto)
    {
        try
        {
            var film = _filmService.FromDto(dto);
            var created = _filmService.Add(film);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ValidationException e)
        {
            return BadRequest(new { errors = e.Errors });
        }
    }

    // PUT api/film/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Film film)
    {
        if (id != film.Id)
            return BadRequest(new { message = "L'id de l'URL ne correspond pas à l'id du body." });

        try
        {
            var updated = _filmService.Update(film);
            return Ok(updated);
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (ValidationException e)
        {
            return BadRequest(new { errors = e.Errors });
        }
    }

    // DELETE api/film/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _filmService.Delete(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }

    /// <summary>
    /// Enrichit un film avec les données TMDB.
    /// </summary>
    [HttpPost("{id}/enrich")]
    public async Task<IActionResult> Enrich(int id, [FromBody] EnrichFilmDto dto)
    {
        try
        {
            var film = _filmService.GetById(id);
            await _tmdbService.EnrichirFilmAsync(film, dto.TmdbId);
            _filmService.Update(film);
            return Ok(_filmService.ToDto(film));
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }
}