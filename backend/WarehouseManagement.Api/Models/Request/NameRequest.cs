using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Api.Models.Request;

public class NameRequest
{
    [Required] public string Name { get; set; } = null!;
}