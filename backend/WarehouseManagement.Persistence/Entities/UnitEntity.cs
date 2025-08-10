using WarehouseManagement.Domain;

namespace WarehouseManagement.Persistence.Entities;

public class UnitEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsArchived { get; set; }
    
    public UnitEntity() { }

    public UnitEntity(string name, bool isArchived = false)
    {
        Name = name;
        IsArchived = isArchived;
    }

    public UnitEntity(Guid id, string name, bool isArchived = false) : this(name, isArchived)
    {
        Id = id;
    }
}