namespace TCPortfolio.Domain.Entities;
/// <summary>
/// Represents a trip, which regroups collection of photos taken during a specific period of time, on specific locations.
/// </summary>
public class Trip {
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<TripTranslation> Translations { get; set; } = new();
    public ICollection<Country>? Countries { get; set; }
}

public class TripTranslation {
    public int Id { get; set; }
    public int TripId { get; set; }
    public string Lang { get; set; } = "en";
    public string? Name { get; set; }
    public string? Description { get; set; }
}