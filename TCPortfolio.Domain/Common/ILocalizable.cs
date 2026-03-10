namespace TCPortfolio.Domain.Common;
/// <summary>
/// Interface for entities that support localization through translations.
/// </summary>
public interface ILocalizable<T> where T : class, ITranslation
{
    ICollection<T> Translations { get; set; }
}