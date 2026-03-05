using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TCPortfolio.Domain.Common;
using TCPortfolio.Domain.Entities;
using TCPortfolio.Infrastructure.Identity;

namespace TCPortfolio.Infrastructure.Data;

/// <summary>
/// The main database context for the application.
/// It manages Identity tables and our custom business entities.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // --- DbSet Definitions ---
    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<PhotoTag> Tags => Set<PhotoTag>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<PhotoTranslation> PhotoTranslations => Set<PhotoTranslation>();
    public DbSet<LocationTranslation> LocationTranslations => Set<LocationTranslation>();
    public DbSet<CountryTranslation> CountryTranslations => Set<CountryTranslation>();
    public DbSet<TripTranslation> TripTranslations => Set<TripTranslation>();
    public DbSet<PhotoLike> PhotoLikes => Set<PhotoLike>();
    public DbSet<PhotoFavorite> PhotoFavorites => Set<PhotoFavorite>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Essential: configures Identity internal tables (AspNetUsers, etc.)
        base.OnModelCreating(modelBuilder);

        // --- 1. AUDITABLE ENTITIES (Shadow Properties) ---
        // Apply CreatedAt/UpdatedAt only to entities implementing IAuditable
        var auditableEntityTypes = modelBuilder.Model.GetEntityTypes()
        .Where(et => typeof(IAuditable).IsAssignableFrom(et.ClrType));
        // Apply Shadow Properties to all entities in your project
        foreach (var entityType in auditableEntityTypes)
        {
            // Add "CreatedAt" and "UpdatedAt" to every table in PostgreSQL
            modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedAt").HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity(entityType.Name).Property<DateTime?>("UpdatedAt");
        }

        // On configure TOUTES les traductions d'un coup (Unique Index sur Lang + ParentId)
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITranslation).IsAssignableFrom(entityType.ClrType))
            {
                // 1. Get the name of the parent ID property (e.g., "PhotoId", "TripId")
                // We look for the property that ends with "Id" but isn't just "Id"
                var fkProperty = entityType.GetProperties()
                    .FirstOrDefault(p => p.Name.EndsWith("Id") && p.Name != "Id");

                if (fkProperty != null)
                {
                    // 2. Create the unique index on [ForeignKey, Lang]
                    entityType.AddIndex(
                    [
                        fkProperty, 
                        entityType.FindProperty("Lang")! 
                    ]).IsUnique = true;
                }
            }
        }

        // --- Photo Configuration ---
        modelBuilder.Entity<Photo>(entity =>
        {
            // Configure Value Objects (Owned Types)
            entity.OwnsOne(p => p.Format);
            entity.OwnsOne(p => p.File);

            // Many-to-Many relationship with Tags (EF Core 5+ handles the join table automatically)
            entity.HasMany(p => p.Tags)
                  .WithMany(t => t.Photos);
        });

        // --- User Interactions (Composite Keys) ---
        // A user can only like/favorite a specific photo once
        modelBuilder.Entity<PhotoLike>()
            .HasKey(l => new { l.UserId, l.PhotoId });

        modelBuilder.Entity<PhotoFavorite>()
            .HasKey(f => new { f.UserId, f.PhotoId });
            
        // Setup Foreign Key for PhotoLike to ApplicationUser (Infrastructure link)
        modelBuilder.Entity<PhotoLike>()
            .HasOne<ApplicationUser>() 
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.UserId);

            // Setup Foreign Key for PhotoLike to ApplicationUser (Infrastructure link)
        modelBuilder.Entity<PhotoFavorite>()
            .HasOne<ApplicationUser>() 
            .WithMany(u => u.Favorites)
            .HasForeignKey(l => l.UserId);
    }
}