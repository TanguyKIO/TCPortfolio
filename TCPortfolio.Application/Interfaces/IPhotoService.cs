using TCPortfolio.Application.DTOs;
using TCPortfolio.Domain.Models;

namespace TCPortfolio.Application.Interfaces;

public interface IPhotoService
{
    Task<IEnumerable<PhotoDto>> GetPhotosAsync(PhotoFilters filters, string lang);
    
    Task<PhotoDto?> GetByIdAsync(Guid id, string lang);
    
    Task<Guid> CreatePhotoAsync(CreateUpdatePhotoDto dto);
    
    Task UpdatePhotoAsync(Guid id, string lang, CreateUpdatePhotoDto dto);
    
    Task DeletePhotoAsync(Guid id);
}