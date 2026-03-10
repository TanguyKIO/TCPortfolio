using Microsoft.EntityFrameworkCore;
using TCPortfolio.Domain.Entities;
using TCPortfolio.Domain.Interfaces;
using TCPortfolio.Domain.Models;
using TCPortfolio.Infrastructure.Data;

namespace TCPortfolio.Infrastructure.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly ApplicationDbContext _context;

    public PhotoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Photo?> GetByIdAsync(Guid id, string lang)
    {
        return await _context.Photos
            .Include(p => p.Trip)
                .ThenInclude(t => t!.Translations.Where(tt => tt.Lang == lang))
            .Include(p => p.Location)
                .ThenInclude(l => l!.Translations.Where(lt => lt.Lang == lang))
            .Include(p => p.Tags)
            .Include(p => p.Translations.Where(t => t.Lang == lang))
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Photo>> GetAllAsync()
    {
        return await _context.Photos
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<IEnumerable<Photo>> GetFilteredAsync(PhotoFilters filters, string lang)
    {
        // No tracking when its photo for gallery
        IQueryable<Photo> query = _context.Photos.AsNoTracking();

        // On inclut les relations nécessaires pour la galerie
        query = query
            .Include(p => p.Translations.Where(t => t.Lang == lang));

        // Adding filters if they exist
        if (filters.TripId.HasValue)
            query = query.Where(p => p.TripId == filters.TripId);

        if (filters.CountryId.HasValue)
            query = query.Where(p => p.Location != null && p.Location.CountryId == filters.CountryId);

        if (filters.DateTaken.HasValue)
            query = query.Where(p => p.DateTaken == filters.DateTaken);

        if (filters.DateFrom.HasValue)
            query = query.Where(p => p.DateTaken >= filters.DateFrom);

        if (filters.DateTo.HasValue)
            query = query.Where(p => p.DateTaken <= filters.DateTo);

        if (!string.IsNullOrEmpty(filters.Tag))
            query = query.Where(p => p.Tags.Any(t => t.Name == filters.Tag));

        // Global search
        if (!string.IsNullOrEmpty(filters.SearchTerm))
        {
            var search = filters.SearchTerm.ToLower();

            query = query.Where(p => 
                // 1. Search in Photo Translations (Title & Description)
                p.Translations.Any(t => t.Lang == lang && (t.Title != null && t.Title.ToLower().Contains(search) || t.Description != null && t.Description.ToLower().Contains(search))) ||
                
                // 2. Search in associated Tags
                p.Tags.Any(tag => tag.Name != null && tag.Name.ToLower().Contains(search)) ||
                
                // 3. Search in Location/Country names
                (p.Location != null && p.Location.Translations.Any(lt => 
                    lt.Lang == lang && lt.Name != null && lt.Name.ToLower().Contains(search)
                )) ||
        
                // 4. Search in Trip titles 
                (p.Trip != null && p.Trip.Translations.Any(tt => 
                    tt.Lang == lang && tt.Name != null && tt.Name.ToLower().Contains(search)
                ))
            );
        }

        return await query.OrderByDescending(p => p.DateTaken).ToListAsync();
    }

    public async Task AddAsync(Photo photo)
    {
        await _context.Photos.AddAsync(photo);
    }

    public void Update(Photo photo)
    {
        _context.Photos.Update(photo);
    }

    public void Delete(Photo photo)
    {
        _context.Photos.Remove(photo);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}