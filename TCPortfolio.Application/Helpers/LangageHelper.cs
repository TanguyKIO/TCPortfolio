public static class LanguageHelper
{
    public static string ExtractLanguage(string? langHeader, string defaultLang = "fr")
    {
        if (string.IsNullOrWhiteSpace(langHeader)) return defaultLang;
        
        try 
        {
            // Prend "fr-FR,fr;q=0.9" -> "fr"
            return langHeader.Split(',')[0].Trim().Substring(0, 2).ToLower();
        }
        catch
        {
            return defaultLang;
        }
    }
}