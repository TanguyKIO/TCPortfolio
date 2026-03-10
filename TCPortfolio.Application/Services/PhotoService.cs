using AutoMapper;
using TCPortfolio.Application.DTOs;
using TCPortfolio.Application.Interfaces;
using TCPortfolio.Domain.Entities;
using TCPortfolio.Domain.Interfaces;
using TCPortfolio.Domain.Models;

/// <summary>
/// Service class responsible for handling business logic related to photos. 
/// It interacts with the IPhotoRepository to perform CRUD operations and uses AutoMapper to map between domain entities and DTOs.
/// </summary>
public class PhotoService : IPhotoService 
{
    private readonly IPhotoRepository _repo;
    private readonly IMapper _mapper;

    public PhotoService(IPhotoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PhotoDto>> GetPhotosAsync(PhotoFilters filters, string lang)
    {
        var photos = await _repo.GetPhotosAsync(filters, lang);
        return _mapper.Map<IEnumerable<PhotoDto>>(photos);
    }

    public async Task<PhotoDto?> GetByIdAsync(Guid id, string lang)
    {
        var photo = await _repo.GetByIdAsync(id, lang);
        if (photo == null) return null;

        var dto = _mapper.Map<PhotoDto>(photo);
        return dto;
    }

    public async Task<Guid> CreatePhotoAsync(CreateUpdatePhotoDto dto)
    {
        var photo = _mapper.Map<Photo>(dto);
        await _repo.AddAsync(photo);
        await _repo.SaveChangesAsync();

        return photo.Id;
    }

    public async Task UpdatePhotoAsync(Guid id, string lang, CreateUpdatePhotoDto dto)
    {
        // 1. Get existing photo with translations
        var existingPhoto = await _repo.GetByIdAsync(id, lang);
        if (existingPhoto == null) throw new KeyNotFoundException("Photo not found");

        // 2. AutoMapper syncs the DTO onto the existing entity, including translations
        _mapper.Map(dto, existingPhoto);

        await _repo.SaveChangesAsync();
    }

    public async Task DeletePhotoAsync(Guid id)
    {
        await _repo.DeleteAsync(id);
        await _repo.SaveChangesAsync();
    }
}