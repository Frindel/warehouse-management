using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Persistence.Entities;

namespace WarehouseManagement.Persistence;

public sealed class DataContext : DbContext
{
    public DbSet<ReceiptDocumentEntity> Receipts { get; set; } = null!;

    public DbSet<ReceiptResourceEntity> ReceiptResources { get; set; } = null!;

    public DbSet<ResourceEntity> Resources { get; set; } = null!;

    public DbSet<UnitEntity> Units { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
}