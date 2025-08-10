using WarehouseManagement.Domain;

namespace WarehouseManagement.Persistence.Entities;

public class ReceiptDocumentEntity
{
    public Guid Id { get; set; }

    public string Number { get; set; } = null!;

    public DateOnly Date { get; set; }

    public List<ReceiptResourceEntity> Resources { get; set; } = null!;
    
    public ReceiptDocumentEntity() { }

    public ReceiptDocumentEntity(string number, DateOnly date, List<ReceiptResourceEntity>? resources = null)
    {
        ArgumentNullException.ThrowIfNull(number);
        
        Number = number;
        Date = date;
        Resources = resources ?? new();
    }

    public ReceiptDocumentEntity(Guid id, string number, DateOnly date, List<ReceiptResourceEntity>? resources = null) : this(number, date, resources)
    {
        Id = id;
    }
}