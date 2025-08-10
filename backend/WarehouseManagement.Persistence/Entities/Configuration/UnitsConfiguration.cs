using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarehouseManagement.Persistence.Entities.Configuration;

public class UnitsConfiguration : IEntityTypeConfiguration<UnitEntity>
{
    public void Configure(EntityTypeBuilder<UnitEntity> builder)
    {
        SetTableProperties(builder);
        SetAttributesProperties(builder);
    }

    void SetTableProperties(EntityTypeBuilder<UnitEntity> builder)
    {
        builder.ToTable("units");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("order_id")
            .ValueGeneratedOnAdd();

        // должно быть уникальное имя
        builder.HasIndex(x => x.Name)
            .IsUnique();
    }

    void SetAttributesProperties(EntityTypeBuilder<UnitEntity> builder)
    {
        // name
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();

        // is_archived
        builder.Property(x => x.IsArchived)
            .HasColumnName("is_archived")
            .IsRequired();
    }
}