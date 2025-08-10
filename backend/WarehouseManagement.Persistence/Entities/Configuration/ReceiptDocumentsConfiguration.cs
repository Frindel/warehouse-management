using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarehouseManagement.Persistence.Entities.Configuration;

public class ReceiptDocumentsConfiguration : IEntityTypeConfiguration<ReceiptDocumentEntity>
{
    public void Configure(EntityTypeBuilder<ReceiptDocumentEntity> builder)
    {
        SetTableProperties(builder);
        SetAttributesProperties(builder);
    }

    void SetTableProperties(EntityTypeBuilder<ReceiptDocumentEntity> builder)
    {
        builder.ToTable("receipt_documents");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("receipt_document_id")
            .ValueGeneratedOnAdd();

        // отношения
        builder.HasMany(x => x.Resources)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }

    void SetAttributesProperties(EntityTypeBuilder<ReceiptDocumentEntity> builder)
    {
        // number
        builder.Property(x => x.Number)
            .HasColumnName("number")
            .IsRequired();

        // date
        builder.Property(x => x.Date)
            .HasColumnName("date")
            .IsRequired();
    }
}