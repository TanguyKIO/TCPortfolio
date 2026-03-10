namespace TCPortfolio.Application.DTOs;

/// <summary>
/// DTO for creating a new photo. It includes all necessary fields to create a photo, such as the main URL, dimensions, optional location and trip associations, and translations for different languages. This DTO is used when adding or updating a new photo to the portfolio.
/// </summary>
public class CreateUpdatePhotoDto
{
    public string MainUrl { get; set; } = string.Empty;
    public string? PublicId { get; set; }
    public DateTime? DateTaken { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Guid? LocationId { get; set; }
    public Guid? TripId { get; set; }
    public List<PhotoTranslationDto> Translations { get; set; } = new();
}

public class PhotoTranslationDto
{
    public string Lang { get; set; } = "en";
    public string? Title { get; set; }
    public string? Description { get; set; }
}