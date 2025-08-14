namespace WarehouseManagement.Api.Models.Response;

public class ReceiptResponse
{
    public Guid Id { get; set; }

    public string Number { get; set; }

    public DateOnly Date { get; set; }

    public List<GetReceiptResourceResponse> Resources { get; set; }
}

public class GetReceiptResourceResponse
{
    public Guid Id { get; set; }

    public Guid Resource { get; set; }

    public string ResourceName { get; set; }

    public Guid Unit { get; set; }

    public string UnitName { get; set; }

    public int Quantity { get; set; }
}