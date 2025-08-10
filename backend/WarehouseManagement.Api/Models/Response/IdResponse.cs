namespace WarehouseManagement.Api.Models.Response;

public class IdResponse
{
    public Guid Id { get; set; }
    
    public IdResponse(Guid id)
    {
        Id = id;
    }
}