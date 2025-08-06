namespace WarehouseManagement.Domain;

class ReceiptResource
{
    /// <summary>
    /// Идентификатор продукта поступления
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор документа поступления
    /// </summary>
    public int ReceiptDocumentId { get; set; }

    /// <summary>
    /// Количество продукта поступления
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Поставляемый ресурс
    /// </summary>
    public Resource Resource { get; set; }

    /// <summary>
    /// Еденица измерения ресурса
    /// </summary>
    public Unit Unit { get; set; }

    public ReceiptResource() { }

    public ReceiptResource(int receiptDocumentId, int quantity, Resource resource, Unit unit)
    {
        ArgumentNullException.ThrowIfNull(resource);
        ArgumentNullException.ThrowIfNull(unit);

        ReceiptDocumentId = receiptDocumentId;
        Quantity = quantity;
    }

    public ReceiptResource(int id, int receiptDocumentId, int quantity, Resource resource, Unit unit) : this(receiptDocumentId, quantity, resource, unit)
    {
        Id = id;
    }
}