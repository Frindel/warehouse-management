namespace WarehouseManagement.Domain;

public class Resource
{
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название ресурса
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Статус ресурса
    /// </summary>
    public bool IsArchived { get; set; }

    public Resource() { }

    public Resource(string name, bool isArcived = false)
    {
        Name = name;
        IsArchived = isArcived;
    }

    public Resource(int id, string name, bool isArcived = false) : this(name, isArcived)
    {
        Id = id;
    }
}
