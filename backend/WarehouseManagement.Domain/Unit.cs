namespace WarehouseManagement.Domain;

class Unit
{
    /// <summary>
    /// Идентификатор еденицы измерения
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название еденицы измерения
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Статус еденицы измерения
    /// </summary>
    public bool IsArchived { get; set; }

    public Unit() { }

    public Unit(string name, bool isArchived)
    {
        Name = name;
        IsArchived = isArchived;
    }

    public Unit(int id, string name, bool isArchived) : this(name, isArchived)
    {
        Id = id;
    }
}