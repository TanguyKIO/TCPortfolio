using Microsoft.AspNetCore.Identity;
using TCPortfolio.Domain.Entities;

namespace TCPortfolio.Infrastructure.Identity;
/// <summary>
/// Custom user entity extending the default IdentityUser.
/// This class will be mapped to the 'AspNetUsers' table.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Custom property for the user's display name.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Collection of photos liked by this user.
    /// </summary>
    public virtual ICollection<PhotoLike> Likes { get; set; } = new List<PhotoLike>();

    /// <summary>
    /// Collection of photos saved as favorites by this user.
    /// </summary>
    public virtual ICollection<PhotoFavorite> Favorites { get; set; } = new List<PhotoFavorite>();
}