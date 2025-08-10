using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Api.Models.Request;

public class UpdateEntityRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public bool IsArchived { get; set; }
}