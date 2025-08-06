namespace WarehouseManagement.Domain;

public class ReceiptDocument
{
    /// <summary>
    /// Идентификатор документа поступления
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Номер документа поступления
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Дата поступления
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Ресурсы, относящиеся к заявке
    /// </summary>
    public List<ReceiptResource> Resources { get; set; } = new();

    public ReceiptDocument() { }

    public ReceiptDocument(string number, DateOnly date, List<ReceiptResource> resources)
    {
        ArgumentNullException.ThrowIfNull(number);
        
        Number = number;
        Date = date;
        Resources = resources;
    }

    public ReceiptDocument(Guid id, string number, DateOnly date, List<ReceiptResource> resources) : this(number, date, resources)
    {
        Id = id;
    }
}