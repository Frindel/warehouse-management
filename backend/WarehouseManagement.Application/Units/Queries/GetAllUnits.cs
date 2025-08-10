using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using Unit = WarehouseManagement.Domain.Unit;

namespace WarehouseManagement.Application.Units.Queries;

public record GetAllUnitsQuery : IRequest<List<Unit>>;

public class GetAllUnitsQueryHandler : IRequestHandler<GetAllUnitsQuery, List<Unit>>
{
    private IUnitsRepository _units;
    
    public GetAllUnitsQueryHandler(IUnitsRepository units)
    {
        _units = units;
    }
    
    public async Task<List<Unit>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
    {
        return await _units.Find();
    }
}