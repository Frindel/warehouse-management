using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarehouseManagement.Persistence.Entities.Configuration;

public class ResourcesConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
        SetTableProperties(builder);
        SetAttributesProperties(builder);
    }
    
    void SetTableProperties(EntityTypeBuilder<ResourceEntity> builder)
    {
        builder.ToTable("resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("resource_id")
            .ValueGeneratedOnAdd();

        // должно быть уникальное имя
        builder.HasIndex(x => x.Name)
            .IsUnique();
    }

    void SetAttributesProperties(EntityTypeBuilder<ResourceEntity> builder)
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