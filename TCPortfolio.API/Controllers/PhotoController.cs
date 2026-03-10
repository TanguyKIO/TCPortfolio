using Microsoft.AspNetCore.Mvc;
using TCPortfolio.Application.Interfaces;
using TCPortfolio.Application.DTOs;
using TCPortfolio.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoService _photoService;
    public PhotosController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPhotos(
        [FromQuery] PhotoFilters filters,
        [FromHeader(Name = "Accept-Language")] string lang = "fr")
    {
        var language = LanguageHelper.ExtractLanguage(lang);
        var photos = await _photoService.GetPhotosAsync(filters, language);

        return Ok(photos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PhotoDto>> GetById(
        Guid id,
        [FromHeader(Name = "Accept-Language")] string lang = "fr")
    {
        var language = LanguageHelper.ExtractLanguage(lang);
        var photo = await _photoService.GetByIdAsync(id, language);

        if (photo == null) return NotFound();
        return Ok(photo);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUpdatePhotoDto dto)
    {
        var photoId = await _photoService.CreatePhotoAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = photoId }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] CreateUpdatePhotoDto dto,
        [FromHeader(Name = "Accept-Language")] string lang = "fr")
    {
        try
        {
            await _photoService.UpdatePhotoAsync(id, lang, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _photoService.DeletePhotoAsync(id);
        return NoContent();
    }
}