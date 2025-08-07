using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using Unit = WarehouseManagement.Domain.Unit;

namespace WarehouseManagement.Application.Units.Commands;

public record CreateUnitCommand : IRequest<Unit>
{
    /// <summary>
    /// Название единицы измерения
    /// </summary>
    public string Name { get; set; } = null!;
}

public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, Unit>
{
    private IUnitsRepository _units;
    
    public CreateUnitCommandHandler(IUnitsRepository units)
    {
        _units = units;
    }
    
    /// <summary>
    ///  Сохраняет новую единицу измерения
    /// </summary>
    /// <returns>Сохраненная иденица измерения</returns>
    /// <exception cref="AlreadyExistsException">Единица с указанным названием уже существует</exception>
    public async Task<Unit> Handle(CreateUnitCommand command, CancellationToken cancellationToken)
    {
        if (await _units.TryGet(command.Name) != null)
            throw new AlreadyExistsException($"Unit with name {command.Name} already exists.");

        Guid unitId = await _units.Create(new Unit(command.Name));
        return (await _units.TryGet(unitId))!;
    }
}