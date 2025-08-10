namespace WarehouseManagement.Domain;

public class Resource
{
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название ресурса
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Статус ресурса
    /// </summary>
    public bool IsArchived { get; set; }

    public Resource() { }

    public Resource(string name, bool isArchived = false)
    {
        Name = name;
        IsArchived = isArchived;
    }

    public Resource(Guid id, string name, bool isArchived = false) : this(name, isArchived)
    {
        Id = id;
    }
}
