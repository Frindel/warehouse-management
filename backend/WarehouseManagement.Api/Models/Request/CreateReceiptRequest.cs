using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Api.Models.Request;

public class CreateReceiptRequest
{
    [Required] public string Number { get; set; } = null!;

    [Required] public DateOnly Date { get; set; }

    public List<CreateReceiptResourceRequest> Resources { get; set; } = new();
}

public class CreateReceiptResourceRequest
{
    [Required] public Guid Resource { get; set; }

    [Required] public Guid Unit { get; set; }

    [Required] public int Quantity { get; set; }
}