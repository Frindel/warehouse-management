using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Receipts.Queries;

public class GetReceiptQuery : IRequest<Receipt>
{
    public Guid Id { get; set; }
}

public class GetReceiptQueryHandler : IRequestHandler<GetReceiptQuery, Receipt>
{
    private readonly IReceiptsRepository _receipts;

    public GetReceiptQueryHandler(IReceiptsRepository receipts)
    {
        _receipts = receipts;
    }

    public async Task<Receipt> Handle(GetReceiptQuery query, CancellationToken cancellationToken)
    {
        var selectedReceipt = await _receipts.TryGet(query.Id);
        if (selectedReceipt == null)
            throw new NotFoundException($"Receipt with id {query.Id} not found.");

        return selectedReceipt;
    }
}