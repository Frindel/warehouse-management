using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Api.Models;
using WarehouseManagement.Api.Models.Request;
using WarehouseManagement.Api.Models.Response;
using WarehouseManagement.Application.Units.Commands;
using WarehouseManagement.Application.Units.Queries;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Api.Controllers;

[Route("/api/units/")]
public class UnitsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
    {
        var query = new GetAllUnitsQuery();
        var units = await Mediator.Send(query);
        return Ok(units);
    }

    [HttpPost]
    public async Task<ActionResult<IdResponse>> CreateUnit([FromBody] NameRequest request)
    {
        var command = new CreateUnitCommand()
        {
            Name = request.Name,
        };

        var newUnit = await Mediator.Send(command);
        return Ok(new IdResponse(newUnit.Id));
    }

    [HttpPut]
    public async Task<ActionResult<IdResponse>> UpdateUnit([FromBody] UpdateEntityRequest request)
    {
        var command = new ChangeUnitCommand()
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
        var command = new DeleteUnitCommand()
        {
            Id = id
        };
        var deletedUnitId = await Mediator.Send(command);
        return Ok(new IdResponse(deletedUnitId));
    }
}