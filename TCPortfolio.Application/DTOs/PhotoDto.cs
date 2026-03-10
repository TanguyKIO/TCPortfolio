namespace TCPortfolio.Application.DTOs;
public class PhotoDto
{
    public Guid Id { get; set; }
    public string MainUrl { get; set; } = string.Empty;
    public DateTime? DateTaken { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsPortrait { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? LocationName { get; set; }
    public string? TripTitle { get; set; }
}