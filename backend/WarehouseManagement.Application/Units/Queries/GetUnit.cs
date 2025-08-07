using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using Unit = WarehouseManagement.Domain.Unit;

namespace WarehouseManagement.Application.Units.Queries;

public record GetUnitQuery : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class GetUnitQueryHandler : IRequestHandler<GetUnitQuery, Unit>
{
    private IUnitsRepository _units;
    
    public GetUnitQueryHandler(IUnitsRepository units)
    {
        _units = units;
    }

    
    public async Task<Unit> Handle(GetUnitQuery query, CancellationToken cancellationToken)
    {
        var receipt = await _units.TryGet(query.Id);
        return receipt ?? throw new NotFoundException($"Unit with id {query.Id} not found");
    }
}