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
        if (resource == null || resource.IsArchived)
            throw new NotFoundException($"Resource with id {id} not found.");
        return resource;
    }

    internal async Task<Unit> GetUnit(Guid id)
    {
        var unit = await _units.TryGet(id);
        if (unit == null || unit.IsArchived)
            throw new NotFoundException($"Unit with id {id} not found.");
        return unit;
    }
}