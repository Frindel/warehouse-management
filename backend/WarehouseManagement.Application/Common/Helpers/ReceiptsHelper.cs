using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Receipts.Helpers;

public class ReceiptsHelper
{
    private readonly IResourcesRepository _resources;
    private readonly IUnitsRepository _units;

    public ReceiptsHelper(IResourcesRepository resources, IUnitsRepository units)
    {
        _resources = resources;
        _units = units;
    }

    internal async Task<Resource> GetResource(Guid id)
    {
        var resource = await _resources.TryGet(id);
        return resource ?? throw new NotFoundException($"Resource with id {id} not found.");
    }

    internal async Task<Unit> GetUnit(Guid id)
    {
        var unit = await _units.TryGet(id);
        return unit ?? throw new NotFoundException($"Unit with id {id} not found.");
    }
}