using System.ComponentModel.DataAnnotations.Schema;
using TCPortfolio.Domain.Common;

namespace TCPortfolio.Domain.Entities;
/// <summary>
/// Represents a specific location, with geographical coordinates and optional descriptive information, that can be associated with a photo, a trip or a user.
/// </summary>
public class Location : IAuditable, ILocalizable<LocationTranslation>
{
    public Guid Id { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public ICollection<LocationTranslation> Translations { get; set; } = [];
    public Guid? CountryId { get; set; }
    [ForeignKey("CountryId")]
    public virtual Country? Country { get; set; }
}
public class LocationTranslation : ITranslation
{
    public Guid Id { get; set; }
    public Guid LocationId { get; set; }
    public string Lang { get; set; } = "en";
    public string? Name { get; set; }
    public string? Description { get; set; }
}