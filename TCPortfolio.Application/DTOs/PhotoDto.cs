namespace TCPortfolio.Application.DTOs;
/// <summary>
/// Représente une photo pour l'affichage dans le portfolio.
/// </summary>
public class PhotoDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}