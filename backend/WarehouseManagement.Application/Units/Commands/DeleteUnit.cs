using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;

namespace WarehouseManagement.Application.Units.Commands;

public record DeleteUnitCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}

public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, Guid>
{
    private IUnitsRepository _units;
    
    public DeleteUnitCommandHandler(IUnitsRepository units)
    {
        _units = units;
    }
    
    public async Task<Guid> Handle(DeleteUnitCommand command, CancellationToken cancellationToken)
    {
        if (!await _units.IsExist(command.Id))
            throw new NotFoundException($"Unit with name {command.Id} not exists.");
        
        if (await _units.IsUse(command.Id))
            throw new InUseException($"Unit with id {command.Id} is use.");

        await _units.Delete(command.Id);
        return command.Id;
    }
}