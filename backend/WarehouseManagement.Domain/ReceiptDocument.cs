namespace WarehouseManagement.Domain;

class ReceiptDocument
{
    /// <summary>
    /// Идентификатор документа поступления
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер документа поступления
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Дата поступления
    /// </summary>
    public DateOnly Date { get; set; }

    public ReceiptDocument() { }

    public ReceiptDocument(string number, DateOnly date)
    {
        Number = number;
        Date = date;
    }

    public ReceiptDocument(int id, string number, DateOnly date) : this(number, date)
    {
        Id = id;
    }
}