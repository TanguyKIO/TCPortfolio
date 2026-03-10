namespace TCPortfolio.Domain.Models;
/// <summary>
/// Represents the various criteria that can be used to filter photos when retrieving them from the repository. 
/// This includes filtering by associated trip, country, tags, date range, and a general search term that can match against multiple fields 
/// (title, description, tags, location, trip). Each property is optional, allowing for flexible combinations of filters when querying photos.
/// </summary>
public record PhotoFilters(
    Guid? TripId = null,
    Guid? CountryId = null,
    string? Tag = null,
    DateTime? DateFrom = null,
    DateTime? DateTaken = null,
    DateTime? DateTo = null,
    string? SearchTerm = null
);