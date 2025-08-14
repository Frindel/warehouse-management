using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Api.Models.Request;

public class UpdateReceiptRequest
{
    [Required] public Guid Id { get; set; }

    [Required] public string Number { get; set; } = null!;

    [Required] public DateOnly Date { get; set; }

    public List<UpdateReceiptResourceRequest> Resources { get; set; } = new();
}

public class UpdateReceiptResourceRequest
{
    public Guid? Id { get; set; }

    [Required] public Guid Resource { get; set; }

    [Required] public Guid Unit { get; set; }

    [Required] public int Quantity { get; set; }
}