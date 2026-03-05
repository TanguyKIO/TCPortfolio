namespace TCPortfolio.Domain.Common;

public interface ILocalizable<T> where T : class, ITranslation
{
    ICollection<T> Translations { get; set; }
}