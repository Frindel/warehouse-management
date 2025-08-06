namespace WarehouseManagement.Domain;

public class Unit
{
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название единицы измерения
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Статус единицы измерения
    /// </summary>
    public bool IsArchived { get; set; }

    public Unit() { }

    public Unit(string name, bool isArchived = false)
    {
        Name = name;
        IsArchived = isArchived;
    }

    public Unit(Guid id, string name, bool isArchived = false) : this(name, isArchived)
    {
        Id = id;
    }
}