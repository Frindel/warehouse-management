using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;

namespace WarehouseManagement.Application.Receipts.Commands;

public record DeleteReceiptCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}

public class DeleteReceiptCommandHandler : IRequestHandler<DeleteReceiptCommand, Guid>
{
    private readonly IReceiptsRepository _receipts;

    public DeleteReceiptCommandHandler(IReceiptsRepository receipts)
    {
        _receipts = receipts;
    }

    public async Task<Guid> Handle(DeleteReceiptCommand command, CancellationToken cancellationToken)
    {
        if (await _receipts.TryGet(command.Id) == null)
            throw new NotFoundException($"Receipt with id {command.Id} not exists.");

        await _receipts.Delete(command.Id);
        return command.Id;
    }
}