namespace TCPortfolio.Domain.Models;
/// <summary>
/// Represents the various criteria that can be used to filter, sort and paginate photos when retrieving them from the repository to show them in an infinite scroll. 
/// This includes filtering by associated trip, country, tags, date range, and a general search term that can match against multiple fields 
/// (title, description, tags, location, trip). Each property is optional, allowing for flexible combinations of filters when querying photos.
/// </summary>
public record PhotoFilters {
    public Guid? TripId { get; init; }
    public Guid? CountryId { get; init; }
    public string? SearchTerm { get; init; }
    public string? Tag { get; init; }
    public DateTime? DateFrom { get; init; }  
    public DateTime? DateTaken { get; init; }  
    public DateTime? DateTo { get; init; }      
    public int PageSize { get; init; } = 20;
    public DateTime? LastDate { get; init; } // For Infinite Scroll
    public Guid? LastId { get; init; }       // For Infinite Scroll
    public PhotoSortOrder SortBy { get; init; } = PhotoSortOrder.DateDesc;
}

public enum PhotoSortOrder
{
    DateDesc, // Newest first
    DateAsc,  // Oldest first
    Country,  // By Country name
    Trip      // By Trip title
}