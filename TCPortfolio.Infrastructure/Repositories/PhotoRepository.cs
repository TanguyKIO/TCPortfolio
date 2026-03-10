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

    public async Task<IEnumerable<Photo>> GetPhotosAsync(PhotoFilters filters, string lang)
    {
        // Start with NoTracking for gallery performance
        IQueryable<Photo> query = _context.Photos.AsNoTracking();

        // 1. DYNAMIC FILTERS 
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

        // 2. GLOBAL SEARCH
        if (!string.IsNullOrEmpty(filters.SearchTerm))
        {
            var search = filters.SearchTerm.ToLower();

            query = query.Where(p => 
                // Search in Photo Translations
                p.Translations.Any(t => t.Lang == lang && (
                    (t.Title != null && t.Title.ToLower().Contains(search)) || 
                    (t.Description != null && t.Description.ToLower().Contains(search))
                )) ||
                
                // Search in associated Tags
                p.Tags.Any(tag => tag.Name != null && tag.Name.ToLower().Contains(search)) ||
                
                // Search in Location translations
                (p.Location != null && p.Location.Translations.Any(lt => 
                    lt.Lang == lang && lt.Name != null && lt.Name.ToLower().Contains(search)
                )) ||
        
                // Search in Trip translations 
                (p.Trip != null && p.Trip.Translations.Any(tt => 
                    tt.Lang == lang && tt.Name != null && tt.Name.ToLower().Contains(search)
                ))
            );
        }

        // We only fetch photos older (or newer) than the last one displayed
        if (filters.LastDate.HasValue && filters.LastId.HasValue)
        {
            if (filters.SortBy == PhotoSortOrder.DateDesc)
                query = query.Where(p => p.DateTaken < filters.LastDate || (p.DateTaken == filters.LastDate && p.Id.CompareTo(filters.LastId) < 0));
            else if (filters.SortBy == PhotoSortOrder.DateAsc)
                query = query.Where(p => p.DateTaken > filters.LastDate || (p.DateTaken == filters.LastDate && p.Id.CompareTo(filters.LastId) > 0));
        }

        // 3. DYNAMIC SORTING
        query = filters.SortBy switch
        {
            PhotoSortOrder.DateAsc => query.OrderBy(p => p.DateTaken).ThenBy(p => p.Id),
            PhotoSortOrder.Country => query.OrderBy(p => p.Location != null && p.Location.Country != null ? p.Location.Country.IsoCode : null).ThenByDescending(p => p.DateTaken),
            PhotoSortOrder.Trip => query.OrderBy(p => p.Trip != null ? p.Trip.Translations.Where(t => t.Lang == lang).Select(t => t.Name).FirstOrDefault() : null).ThenByDescending(p => p.DateTaken),
            _ => query.OrderByDescending(p => p.DateTaken).ThenByDescending(p => p.Id) // Default: DateDesc
        };

        // 4. PROJECTION & LIMIT (Take only PageSize)
        return await query
            .Take(filters.PageSize)
            .ToListAsync();
    }

    public async Task AddAsync(Photo photo)
    {
        await _context.Photos.AddAsync(photo);
    }

    public async Task DeleteAsync(Guid id)
    {
        var photo = await _context.Photos.FindAsync(id);
        if (photo != null)
        {
            _context.Photos.Remove(photo);
        }
    }

    public async Task UpdateAsync(Photo photo)
    {
        _context.Photos.Update(photo);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}