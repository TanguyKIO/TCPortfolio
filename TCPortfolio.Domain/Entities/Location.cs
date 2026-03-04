namespace TCPortfolio.Domain.Entities;
/// <summary>
/// Represents a specific location, with geographical coordinates and optional descriptive information, that can be associated with a photo, a trip or a user.
/// </summary>
public class Location
{
    public Guid Id { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public List<LocationTranslation> Translations { get; set; } = [];
    public Country? Country { get; set; }
}
public class LocationTranslation {
    public int Id { get; set; }
    public int LocationId { get; set; }
    public string Lang { get; set; } = "en";
    public string? Name { get; set; }
    public string? Description { get; set; }
}