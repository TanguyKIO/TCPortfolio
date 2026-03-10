namespace TCPortfolio.Domain.Common;
/// <summary>
/// Interface for translation entities, which represent localized versions of text fields for different languages. 
/// Each translation has a language code (e.g., "en", "fr") and the translated name or text.
/// </summary>
public interface ITranslation {
    string Lang { get; set; }
}