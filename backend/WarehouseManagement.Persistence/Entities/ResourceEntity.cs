using WarehouseManagement.Domain;

namespace WarehouseManagement.Persistence.Entities;

public class ResourceEntity : Resource
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsArchived { get; set; }
    
    public ResourceEntity() { }

    public ResourceEntity(string name, bool isArchived = false)
    {
        Name = name;
        IsArchived = isArchived;
    }

    public ResourceEntity(Guid id, string name, bool isArchived = false) : this(name, isArchived)
    {
        Id = id;
    }
}