namespace TCPortfolio.Domain.Entities;
/// <summary>
/// Represents the format of a photo, including its dimensions and aspect ratio, which can be used to determine how the photo should be displayed or categorized (e.g., portrait, landscape, square).
/// </summary>
public class PhotoFormat
{
    public double Width { get; set; }
    public double Height { get; set; }
    public bool IsPortrait => Height > Width;
    public bool IsLandscape => Width > Height;
    public bool IsSquare => Width == Height;
    public double AspectRatio => Height != 0 ? Width / Height : 0;
}