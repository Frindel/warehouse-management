using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Api.Models;
using WarehouseManagement.Api.Models.Request;
using WarehouseManagement.Api.Models.Response;
using WarehouseManagement.Application.Resources.Commands;
using WarehouseManagement.Application.Resources.Queries;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Api.Controllers;

[Route("/api/resources/")]
public class ResourcesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
    {
        var query = new GetAllResourcesQuery();
        var resources = await Mediator.Send(query);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<ActionResult<IdResponse>> CreateUnit([FromBody] NameRequest request)
    {
        var command = new CreateResourceCommand()
        {
            Name = request.Name,
        };

        var newUnit = await Mediator.Send(command);
        return Ok(new IdResponse(newUnit.Id));
    }

    [HttpPut]
    public async Task<ActionResult<IdResponse>> UpdateUnit([FromBody] UpdateEntityRequest request)
    {
        var command = new ChangeResourceCommand()
        {
            Id = request.Id,
            Name = request.Name,
            IsArchived = request.IsArchived,
        };
        var changedUnit = await Mediator.Send(command);
        return Ok(new IdResponse(changedUnit.Id));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<IdResponse>> DeleteUnit([FromRoute] Guid id)
    {
        var command = new DeleteResourceCommand()
        {
            Id = id
        };
        var deletedUnitId = await Mediator.Send(command);
        return Ok(new IdResponse(deletedUnitId));
    }
}