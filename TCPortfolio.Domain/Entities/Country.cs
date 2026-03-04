namespace TCPortfolio.Domain.Entities;
/// <summary>
/// Represents a country, including its ISO code, emoji flag, and translations for its name in different languages. This can be used to associate photos or trips with specific countries and to display country information in the user's preferred language.
/// </summary>
public class Country
{
    public int Id { get; set; }
    public string IsoCode { get; set; } = string.Empty;
    public string Emoji { get; set; } = string.Empty;
    public List<CountryTranslation> Translations { get; set; } = [];
}

public class CountryTranslation
{
    public int Id { get; set; }
    public int CountryId { get; set; } // Clé étrangère
    public string Lang { get; set; } = "en"; // "fr", "en", etc.
    public string Name { get; set; } = string.Empty; // Le nom traduit
}