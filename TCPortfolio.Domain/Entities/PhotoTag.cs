using TCPortfolio.Domain.Common;
namespace TCPortfolio.Domain.Entities;
/// <summary>
/// Represents a tag that can be associated with photos, allowing for categorization and easier searching. Each tag has a name and can be linked to multiple photos, while each photo can have multiple tags (many-to-many relationship).
/// </summary>
public class PhotoTag : IAuditable
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Photo> Photos { get; set; } = [];
}
