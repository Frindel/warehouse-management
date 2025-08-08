using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Receipts.Queries;

public record FindReceiptsQuery : IRequest<List<Receipt>>
{
    public List<string>? Numbers { get; set; } = new();

    public (DateOnly begin, DateOnly end)? Period { get; set; }

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
        return await _receipts.Find(query.Numbers, query.Period, query.UnitsId, query.ProductIds);
    }
}