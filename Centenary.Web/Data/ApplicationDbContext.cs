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

        builder.Entity<DocumentDto>()
            .ToTable("Document")
            .HasIndex(u => new { u.FullPath })
            .IsUnique();
    }

    public DbSet<DocumentDto> Documents { get; set; } = null!;
}