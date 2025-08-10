using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarehouseManagement.Persistence.Entities.Configuration;

public class ReceiptResourcesConfiguration : IEntityTypeConfiguration<ReceiptResourceEntity>
{
    public void Configure(EntityTypeBuilder<ReceiptResourceEntity> builder)
    {
        SetTableProperties(builder);
    }

    void SetTableProperties(EntityTypeBuilder<ReceiptResourceEntity> builder)
    {
        builder.ToTable("receipt_resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("receipt_resource_id")
            .ValueGeneratedOnAdd();

        // отношения
        builder.HasOne(x => x.Unit)
            .WithMany()
            .HasForeignKey(x => x.UnitId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Resource)
            .WithMany()
            .HasForeignKey(x => x.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    void SetAttributesProperties(EntityTypeBuilder<ReceiptResourceEntity> builder)
    {
        // quantity
        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .IsRequired();
    }
}