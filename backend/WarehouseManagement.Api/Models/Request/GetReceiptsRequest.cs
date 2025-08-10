namespace WarehouseManagement.Api.Models.Request;

public class GetReceiptsRequest
{
    public DateOnly? From { get; set; }
    
    public DateOnly? To { get; set; }

    public List<string>? Numbers { get; set; }

    public List<Guid>? ProductIds { get; set; }

    public List<Guid>? UnitsIds { get; set; }
}