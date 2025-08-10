using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Api.Models;
using WarehouseManagement.Api.Models.Request;
using WarehouseManagement.Api.Models.Response;
using WarehouseManagement.Application.Receipts.Commands;
using WarehouseManagement.Application.Receipts.Queries;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Api.Controllers;

[Route("/api/receipts/")]
public class ReceiptsController : BaseController
{
    [HttpPost("find")]
    public async Task<ActionResult<List<GetReceiptsResponse>>> GetReceipts([FromBody] GetReceiptsRequest? request)
    {
        var query = new FindReceiptsQuery();
        if (request != null)
        {
            query.Numbers = request.Numbers;
            query.From = request.From;
            query.To = request.To;
            query.UnitsId = request.UnitsIds;
            query.ProductIds = request.ProductIds;
        }

        var receipts = await Mediator.Send(query);
        var response = receipts.Select(r => new GetReceiptsResponse()
        {
            Id = r.Id,
            Number = r.Number,
            Date = r.Date,
            Resources = r.Resources.Select(res => new GetReceiptResourceResponse()
            {
                Id = res.Id,
                Quantity = res.Quantity,
                ResourceName = res.Resource.Name,
                ResourceId = res.Resource.Id,
                UnitName = res.Unit.Name,
                UnitId = res.Unit.Id,
            }).ToList()
        });
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<IdResponse>> CreateReceipt([FromBody] CreateReceiptRequest request)
    {
        var query = new CreateReceiptCommand()
        {
            Number = request.Number,
            Date = request.Date,
            Resources = request.Resources.Select(r => new CreatingReceiptResource()
            {
                ResourceId = r.ResourceId,
                Quantity = r.Quantity,
                UnitId = r.UnitId,
            }).ToList()
        };

        var createdReceipt = await Mediator.Send(query);
        return Ok(new IdResponse(createdReceipt.Id));
    }

    [HttpPut]
    public async Task<ActionResult<IdResponse>> UpdateReceipt([FromBody] UpdateReceiptRequest request)
    {
        var command = new ChangeReceiptCommand()
        {
            Id = request.Id,
            Number = request.Number,
            Date = request.Date,
            Resources = request.Resources.Select(r => new ChangingReceiptResource()
            {
                Id = r.Id,
                ResourceId = r.ResourceId,
                UnitId = r.UnitId,
                Quantity = r.Quantity,
            }).ToList()
        };

        var updatedReceipt = await Mediator.Send(command);
        return Ok(new IdResponse(updatedReceipt.Id));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<IdResponse>> DeleteReceipt([FromRoute] Guid id)
    {
        var command = new DeleteReceiptCommand()
        {
            Id = id
        };

        var deletedReceiptId = await Mediator.Send(command);
        return Ok(new IdResponse(deletedReceiptId));
    }
}