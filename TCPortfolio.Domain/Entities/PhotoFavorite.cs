namespace TCPortfolio.Domain.Entities;

/// <summary>
/// Represents a 'Favorite' save. 
/// Used for the user's private collection/bookmarks.
/// </summary>
public class PhotoFavorite
{
    // Composite Key: UserId + PhotoId
    public string UserId { get; set; } = string.Empty;
    public Guid PhotoId { get; set; }
    
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual Photo Photo { get; set; } = null!;
}