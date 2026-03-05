using TCPortfolio.Domain.Common;

namespace TCPortfolio.Domain.Entities;
/// <summary>
/// Represents a trip, which regroups collection of photos taken during a specific period of time, on specific locations.
/// </summary>
public class Trip : IAuditable , ILocalizable<TripTranslation>
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<TripTranslation> Translations { get; set; } = [];
    public ICollection<Country>? Countries { get; set; }
}

public class TripTranslation : ITranslation
{
    public Guid Id { get; set; }
    public Guid TripId { get; set; }
    public string Lang { get; set; } = "en";
    public string? Name { get; set; }
    public string? Description { get; set; }
}