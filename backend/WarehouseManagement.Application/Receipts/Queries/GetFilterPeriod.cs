using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Receipts.Dto;

namespace WarehouseManagement.Application.Receipts.Queries;

public record GetFilterOptionsQuery : IRequest<ReceiptFilterOptionsDto>;

public class GetFilterPeriodQueryHandler : IRequestHandler<GetFilterOptionsQuery, ReceiptFilterOptionsDto>
{
    private readonly IReceiptsRepository _receipts;

    public GetFilterPeriodQueryHandler(IReceiptsRepository receipts)
    {
        _receipts = receipts;
    }

    public async Task<ReceiptFilterOptionsDto> Handle(GetFilterOptionsQuery request,
        CancellationToken cancellationToken)
    {
        var period = await _receipts.GetMaxPeriod();
        var receipts = await _receipts.GetOnlyReceipts();
        return new ReceiptFilterOptionsDto()
        {
            From = period.begin,
            To = period.end,
            Receipts = receipts.Select(receipt => new ReceiptInfoDto()
            {
                Id = receipt.Id,
                Number = receipt.Number
            }).ToList()
        };
    }
}