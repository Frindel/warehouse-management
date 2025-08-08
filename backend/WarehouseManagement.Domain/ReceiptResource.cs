namespace WarehouseManagement.Domain;

public class ReceiptResource : ICloneable
{
    /// <summary>
    /// Идентификатор продукта поступления
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Количество продукта поступления
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Поставляемый ресурс
    /// </summary>
    public Resource Resource { get; set; }

    /// <summary>
    /// Единица измерения ресурса
    /// </summary>
    public Unit Unit { get; set; }

    public ReceiptResource()
    {}
    
    public ReceiptResource(int quantity, Resource resource, Unit unit)
    {
        ArgumentNullException.ThrowIfNull(resource);
        ArgumentNullException.ThrowIfNull(unit);

        Quantity = quantity;
        Resource = resource;
        Unit = unit;
    }

    public ReceiptResource(Guid id, int quantity, Resource resource, Unit unit) : this(quantity, resource, unit)
    {
        Id = id;
    }

    public object Clone()
    {
        return new ReceiptResource(Id, Quantity, Resource, Unit);
    }
}