using Microsoft.EntityFrameworkCore;
using TCPortfolio.Domain.Entities;

namespace TCPortfolio.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    // C'est cette ligne qui créera la table "Photos" en base de données
    public DbSet<Photo> Photos => Set<Photo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // C'est ici qu'on applique SOLID : on peut configurer finement 
        // les contraintes de la DB sans toucher à la classe Photo du Domain.
        modelBuilder.Entity<Photo>(entity => {
            entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
            entity.Property(p => p.ImageUrl).IsRequired();
        });
    }
}