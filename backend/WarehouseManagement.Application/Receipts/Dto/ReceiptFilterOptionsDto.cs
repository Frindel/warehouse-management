namespace WarehouseManagement.Application.Receipts.Dto;

public class ReceiptFilterOptionsDto
{
    public DateOnly From { get; set; }
    
    public DateOnly To { get; set; }

    public List<ReceiptInfoDto> Receipts { get; set; } = null!;
}