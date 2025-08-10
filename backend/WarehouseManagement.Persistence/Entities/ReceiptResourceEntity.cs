using WarehouseManagement.Domain;

namespace WarehouseManagement.Persistence.Entities;

public class ReceiptResourceEntity
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public ResourceEntity Resource { get; set; } = null!;

    public Guid ResourceId { get; set; }

    public UnitEntity Unit { get; set; } = null!;
    
    public Guid UnitId { get; set; }
    
    public ReceiptResourceEntity()
    {}
    
    public ReceiptResourceEntity(int quantity, ResourceEntity resource, UnitEntity unit)
    {
        ArgumentNullException.ThrowIfNull(resource);
        ArgumentNullException.ThrowIfNull(unit);

        Quantity = quantity;
        Resource = resource;
        Unit = unit;
        
        UnitId = unit.Id;
        ResourceId = resource.Id;
    }

    public ReceiptResourceEntity(Guid id, int quantity, ResourceEntity resource, UnitEntity unit) : this(quantity, resource, unit)
    {
        Id = id;
    }
}