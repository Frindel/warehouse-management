using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Receipts.Queries;

public record FindReceiptsQuery : IRequest<List<Receipt>>
{
    public List<string>? Numbers { get; set; }

    public DateOnly? From { get; set; }

    public DateOnly? To { get; set; }

    public List<Guid>? UnitsId { get; set; }

    public List<Guid>? ProductIds { get; set; }
}

public class FindReceiptsQueryHandler : IRequestHandler<FindReceiptsQuery, List<Receipt>>
{
    private readonly IReceiptsRepository _receipts;

    public FindReceiptsQueryHandler(IReceiptsRepository receipts)
    {
        _receipts = receipts;
    }

    public async Task<List<Receipt>> Handle(FindReceiptsQuery query, CancellationToken cancellationToken)
    {
        var period = (query.From.HasValue && query.To.HasValue)
            ? (begin: query.From!.Value, end: query.To!.Value)
            : ((DateOnly begin, DateOnly end)?)null;
        return await _receipts.Find(query.Numbers, period, query.UnitsId,
            query.ProductIds);
    }
}