namespace WarehouseManagement.Domain;

public class Receipt
{
    /// <summary>
    /// Идентификатор поступления
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Номер поступления
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Дата поступления
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Ресурсы, относящиеся к заявке
    /// </summary>
    public List<ReceiptResource> Resources { get; set; }

    public Receipt() { }

    public Receipt(string number, DateOnly date, List<ReceiptResource>? resources = null)
    {
        ArgumentNullException.ThrowIfNull(number);
        
        Number = number;
        Date = date;
        Resources = resources ?? new();
    }

    public Receipt(Guid id, string number, DateOnly date, List<ReceiptResource>? resources = null) : this(number, date, resources)
    {
        Id = id;
    }
}