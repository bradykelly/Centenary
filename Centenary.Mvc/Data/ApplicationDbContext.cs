using Centenary.Mvc.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Centenary.Mvc.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Folder>()
            .HasOne(f => f.Parent)
            .WithMany(f => f.Folders)
            .HasForeignKey(f => f.ParentId);
        
        builder.Entity<Document>()
            .HasIndex(u => new { u.Name, u.FolderId })
            .IsUnique();
        
        // builder.Entity<Document>()
        //     .HasOne(d => d.Folder)
        //     .WithMany(f => f.Documents)
        //     .HasForeignKey(d => d.FolderId);
    }

    public DbSet<Folder> Folders { get; set; }
    public DbSet<Document> Documents { get; set; } = null!;
}