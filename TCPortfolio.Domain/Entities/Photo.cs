namespace TCPortfolio.Domain.Entities;
/// <summary>
/// Represents a photo, including its metadata such as the date it was taken, its location, the trip it belongs to, its format (dimensions and aspect ratio), and associated tags. This entity serves as the core representation of a photo in the application, allowing for organization, categorization, and display of photos in various contexts (e.g., galleries, albums, search results).
/// </summary>
public class Photo
{
    public Guid Id { get; set; }
    public DateTime? DateTaken { get; set; }
    public virtual Location? Location { get; set; }
    public virtual Trip? Trip { get; set; }
    public PhotoFormat? Format { get; set; }
    public virtual ICollection<PhotoTag> Tags { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }    
    public virtual List<PhotoTranslation> Translations { get; set; } = [];
}
public class PhotoTranslation
{
    public int Id {get; set;}
    public int IdPhoto {get; set;}
    public string Language {get; set;} = "en";
    public string? Title {get; set;}
    public string? Description {get; set;}
}