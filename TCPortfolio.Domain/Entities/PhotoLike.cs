namespace TCPortfolio.Domain.Entities;

/// <summary>
/// Represents a 'Like' interaction. 
/// Used for global statistics and social proof.
/// </summary>
public class PhotoLike
{
    // Composite Key: UserId + PhotoId
    public string UserId { get; set; } = string.Empty;
    public Guid PhotoId { get; set; }
    
    public DateTime LikedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual Photo Photo { get; set; } = null!;
}