using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Persistence;

public class DataContext : DbContext
{
    public DbSet<Receipt> Receipts { get; set; } = null!;

    public DbSet<ReceiptResource> ReceiptResources { get; set; } = null!;

    public DbSet<Resource> Resources { get; set; } = null!;

    public DbSet<Unit> Units { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder) =>
    //     modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ReceiptDocuments
        
        modelBuilder.Entity<Receipt>()
            .HasKey(r => r.Id);
        
        modelBuilder.Entity<Receipt>()
            .ToTable("receipt_documents");

        modelBuilder.Entity<Receipt>()
            .HasMany(r => r.Resources)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        // ReceiptResources
        
        modelBuilder.Entity<ReceiptResource>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<ReceiptResource>()
            .HasOne(r => r.Resource)
            .WithMany()
            .HasForeignKey("ResourceId")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ReceiptResource>()
            .HasOne(r => r.Unit)
            .WithMany()
            .HasForeignKey("UnitId")
            .OnDelete(DeleteBehavior.Restrict);

        // Resources
        
        modelBuilder.Entity<Resource>()
            .HasKey(r => r.Id);
        
        modelBuilder.Entity<Resource>()
            .HasIndex(u => u.Name)
            .IsUnique();

        // Units
        
        modelBuilder.Entity<Unit>()
            .HasKey(u => u.Id);
        
        modelBuilder.Entity<Unit>()
            .HasIndex(u => u.Name)
            .IsUnique();
    }
}