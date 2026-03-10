using System.ComponentModel.DataAnnotations.Schema;
using TCPortfolio.Domain.Common;

namespace TCPortfolio.Domain.Entities;
/// <summary>
/// Represents a photo, including its metadata such as the date it was taken, its location, the trip it belongs to, its format (dimensions and aspect ratio), and associated tags. This entity serves as the core representation of a photo in the application, allowing for organization, categorization, and display of photos in various contexts (e.g., galleries, albums, search results).
/// </summary>
public class Photo : IAuditable, ILocalizable<PhotoTranslation>
{
    public Guid Id { get; set; }
    public DateTime? DateTaken { get; set; }
    public string MainUrl { get; set; } = string.Empty; // Cloudinary URL to the main photo (full size)
    public string? PublicId { get; set; } // Cloudinary public ID
    public int Width { get; set; }
    public int Height { get; set; }    
    public bool IsPortrait => Height > Width; // Not mapped to DB, calculated on the fly      
    public double AspectRatio => Height != 0 ? (double)Width / Height : 0; // Not mapped to DB, calculated on the fly 
    public Guid? LocationId { get; set; }
    [ForeignKey("LocationId")]
    public virtual Location? Location { get; set; }
    public Guid? TripId { get; set; }
    [ForeignKey("TripId")]
    public virtual Trip? Trip { get; set; }
    public virtual ICollection<PhotoTag> Tags { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }    
    public virtual ICollection<PhotoTranslation> Translations { get; set; } = [];
}
public class PhotoTranslation : ITranslation
{
    public Guid Id {get; set;}
    public Guid PhotoId {get; set;}
    public string Lang {get; set;} = "en";
    public string? Title {get; set;}
    public string? Description {get; set;}
}