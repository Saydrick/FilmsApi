using FilmsApi.Api.Exceptions;
using FilmsApi.Api.Models;
using FilmsApi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmsApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeriesController : ControllerBase
{
    private readonly SerieService _seriesService;

    public SeriesController(SerieService serieService)
    {
        _seriesService = serieService;
    }

    // GET api/series
    [HttpGet]
    public IActionResult GetAll()
    {
        var series = _seriesService.GetAll();
        return Ok(series);
    }

    // GET api/series/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var series = _seriesService.GetById(id);
            return Ok(series);
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }

    // POST api/series
    [HttpPost]
    public IActionResult Add([FromBody] Serie serie)
    {
        try
        {
            var created = _seriesService.Add(serie);
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
            var updated = _seriesService.Update(serie);
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
            _seriesService.Delete(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }
}
