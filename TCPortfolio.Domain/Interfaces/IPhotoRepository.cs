using TCPortfolio.Domain.Entities;
using TCPortfolio.Domain.Models;
namespace TCPortfolio.Domain.Interfaces;
/// <summary>
/// Interface for managing photo data, including retrieval, addition, updating, and deletion of photos. It also includes methods for filtering photos based on various criteria and saving changes to the data store.
/// </summary>
public interface IPhotoRepository
{
    Task<Photo?> GetByIdAsync(Guid id, string lang);
    Task<IEnumerable<Photo>> GetAllAsync();
    Task<IEnumerable<Photo>> GetFilteredAsync(PhotoFilters filters, string lang);
    Task AddAsync(Photo photo);
    void Update(Photo photo);
    void Delete(Photo photo);
    Task<bool> SaveChangesAsync();
}