using FilmsApi.Api.DTOs;
using FilmsApi.Api.Exceptions;
using FilmsApi.Api.Models;
using FilmsApi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmsApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeriesController : ControllerBase
{
    private readonly SerieService _serieService;
    private readonly TmdbService _tmdbService;

    public SeriesController(SerieService serieService, TmdbService tmdbService)
    {
        _serieService = serieService;
        _tmdbService = tmdbService;
    }

    // GET api/series
    [HttpGet]
    public IActionResult GetAll()
    {
        var series = _serieService.GetAll()
            .Select(s => _serieService.ToDto(s));
        return Ok(series);
    }

    // GET api/series/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var series = _serieService.GetById(id);
            return Ok(_serieService.ToDto(series));
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }

    // POST api/series
    [HttpPost]
    public IActionResult Add([FromBody] CreateSerieDto dto)
    {
        try
        {
            var serie = _serieService.FromDto(dto);
            var created = _serieService.Add(serie);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ValidationException e)
        {
            return BadRequest(new { errors = e.Errors });
        }
    }

    // PUT api/series/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Serie serie)
    {
        if (id != serie.Id)
            return BadRequest(new { message = "L'id de l'URL ne correspond pas à l'id du body." });

        try
        {
            var updated = _serieService.Update(serie);
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

    // DELETE api/series/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _serieService.Delete(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }

    /// <summary>
    /// Enrichit une serie avec les données TMDB.
    /// </summary>
    [HttpPost("{id}/enrich")]
    public async Task<IActionResult> Enrich(int id, [FromBody] EnrichSerieDto dto)
    {
        try
        {
            var serie = _serieService.GetById(id);
            await _tmdbService.EnrichirSerieAsync(serie, dto.TmdbId);
            _serieService.Update(serie);
            return Ok(_serieService.ToDto(serie));
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }
}
