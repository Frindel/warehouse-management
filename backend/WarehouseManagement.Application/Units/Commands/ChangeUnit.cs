using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using Unit = WarehouseManagement.Domain.Unit;

namespace WarehouseManagement.Application.Units.Commands;

public record ChangeUnitCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public bool IsArchived { get; set; }
}

public class ChangeUnitCommandHandler : IRequestHandler<ChangeUnitCommand, Unit>
{
    private IUnitsRepository _units;
    
    public ChangeUnitCommandHandler(IUnitsRepository units)
    {
        _units = units;
    }
    
    public async Task<Unit> Handle(ChangeUnitCommand command, CancellationToken cancellationToken)
    {
        if (await _units.TryGet(command.Id) == null)
            throw new NotFoundException($"Unit with id {command.Id} not exists.");
        
        await _units.Update(new Unit(command.Id, command.Name, command.IsArchived));
        return (await _units.TryGet(command.Id))!;
    }
}