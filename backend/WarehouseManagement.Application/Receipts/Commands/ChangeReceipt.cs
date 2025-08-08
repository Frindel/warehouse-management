using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Receipts.Helpers;
using WarehouseManagement.Domain;
using Unit = WarehouseManagement.Domain.Unit;

namespace WarehouseManagement.Application.Receipts.Commands;

public record ChangeReceiptCommand : IRequest<Receipt>
{
    public Guid Id { get; set; }

    public string Number { get; set; } = null!;

    public DateOnly Date { get; set; }

    public List<ChangingReceiptResource> Resources { get; set; } = new();
}

public class ChangingReceiptResource
{
    public Guid? Id { get; set; }

    public Guid ResourceId { get; set; }

    public Guid UnitId { get; set; }

    public int Quantity { get; set; }
}

public class ChangeReceiptCommandHandler : IRequestHandler<ChangeReceiptCommand, Receipt>
{
    private readonly IReceiptsRepository _receipts;
    private readonly ReceiptsHelper _helper;

    public ChangeReceiptCommandHandler(IReceiptsRepository receipts, ReceiptsHelper helpers)
    {
        _receipts = receipts;
        _helper = helpers;
    }

    public async Task<Receipt> Handle(ChangeReceiptCommand command, CancellationToken cancellationToken)
    {
        var receipt = await _receipts.TryGet(command.Number);
        if (receipt == null)
            throw new AlreadyExistsException($"Receipt with number {command.Number} already exists.");

        await ChangeReceipt(receipt, command.Number, command.Date);
        receipt.Resources = await UpdateReceiptResources(receipt.Resources, command.Resources);

        await _receipts.Update(receipt);
        return (await _receipts.TryGet(command.Id))!;
    }

    async Task ChangeReceipt(Receipt receipt, string number, DateOnly date)
    {
        if (receipt.Number != number && await _receipts.TryGet(receipt.Number) != null)
            throw new AlreadyExistsException($"Receipt with number {number} already exists.");

        receipt.Number = number;
        receipt.Date = date;
    }

    private async Task<List<ReceiptResource>> UpdateReceiptResources(
        List<ReceiptResource> existingResources,
        List<ChangingReceiptResource> newResources)
    {
        var updatedResources = new List<ReceiptResource>();

        foreach (var newResource in newResources)
        {
            var resource = await _helper.GetResource(newResource.ResourceId);
            var unit = await _helper.GetUnit(newResource.UnitId);

            updatedResources.Add(newResource.Id.HasValue
                ? UpdateExistingResource(existingResources, newResource, resource, unit)
                : new ReceiptResource(newResource.Quantity, resource, unit));
        }

        updatedResources.AddRange(existingResources
            .Where(r => !newResources.Any(nr => nr.Id.HasValue && nr.Id == r.Id)));

        return updatedResources;
    }

    private ReceiptResource UpdateExistingResource(
        List<ReceiptResource> existingResources,
        ChangingReceiptResource newResource,
        Resource resource,
        Unit unit)
    {
        var existingResource = existingResources.FirstOrDefault(r => r.Id == newResource.Id!.Value)
                               ?? throw new NotFoundException($"Receipt resource with ID {newResource.Id} not found.");

        var updatedResource = (ReceiptResource)existingResource.Clone();
        updatedResource.Quantity = newResource.Quantity;
        updatedResource.Resource = resource;
        updatedResource.Unit = unit;
        return updatedResource;
    }
}