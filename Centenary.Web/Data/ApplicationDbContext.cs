using Centenary.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Centenary.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<FolderDto>()
            .ToTable("Folder")
            .HasIndex(f => f.Path)
            .IsUnique();

        builder.Entity<DocumentDto>()
            .ToTable("Document")
            .HasIndex(u => new { u.Name, u.FolderPath })
            .IsUnique();
        
        builder.Entity<DocumentDto>()
            .HasOne(d => d.Folder)
            .WithMany(f => f.Documents)
            .HasForeignKey(d => d.FolderPath)
            .HasPrincipalKey( f => f.Path)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<FolderDto> Folders { get; set; } = null!;
    public DbSet<DocumentDto> Documents { get; set; } = null!;
}