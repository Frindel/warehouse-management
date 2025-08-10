using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Receipts.Helpers;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Receipts.Commands;

public record CreateReceiptCommand : IRequest<Receipt>
{
    public string Number { get; set; } = null!;

    public DateOnly Date { get; set; }

    public List<CreatingReceiptResource>? Resources { get; set; }
}

public class CreatingReceiptResource
{
    public Guid ResourceId { get; set; }

    public Guid UnitId { get; set; }

    public int Quantity { get; set; }
}

public class CreateReceiptCommandHandler : IRequestHandler<CreateReceiptCommand, Receipt>
{
    private readonly IReceiptsRepository _receipts;
    private readonly ReceiptsHelper _helper;

    public CreateReceiptCommandHandler(IReceiptsRepository receipts, ReceiptsHelper helper)
    {
        _receipts = receipts;
        _helper = helper;
    }

    public async Task<Receipt> Handle(CreateReceiptCommand command, CancellationToken cancellationToken)
    {
        if (await _receipts.TryGet(command.Number) != null)
            throw new AlreadyExistsException($"Receipt with number {command.Number} already exists.");

        var resources = await AssembleReceiptResources(command.Resources ?? new List<CreatingReceiptResource>());
        var receipt = new Receipt(command.Number, command.Date, resources);

        var receiptId = await _receipts.Create(receipt);
        return (await _receipts.TryGet(receiptId))!;
    }
    
    async Task<List<ReceiptResource>> AssembleReceiptResources(List<CreatingReceiptResource> resources)
    {
        var receiptResources = new List<ReceiptResource>();
        foreach (var dto in resources)
        {
            var resource = await _helper.GetResource(dto.ResourceId);
            var unit = await _helper.GetUnit(dto.UnitId);
            receiptResources.Add(new ReceiptResource(dto.Quantity, resource, unit));
        }

        return receiptResources;
    }
}