using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Domain;
using WarehouseManagement.Persistence.Entities;

namespace WarehouseManagement.Persistence.Implementations;

public class UnitsRepository : IUnitsRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UnitsRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Create(Unit unit)
    {
        var unitEntity = _mapper.Map<UnitEntity>(unit);
        _context.Units.Add(unitEntity);
        await _context.SaveChangesAsync();

        ClearTracking();
        return unitEntity.Id;
    }

    public async Task<Unit?> TryGet(string name)
    {
        var unitEntity = await _context.Units.FirstOrDefaultAsync(u => u.Name == name);
        if (unitEntity == null)
            return null;

        ClearTracking();
        return _mapper.Map<Unit>(unitEntity);
    }

    public async Task<Unit?> TryGet(Guid id)
    {
        var unitEntity = await _context.Units.FindAsync(id);

        ClearTracking();
        return _mapper.Map<Unit>(unitEntity);
    }

    public async Task Update(Unit unit)
    {
        var unitEntity = _mapper.Map<UnitEntity>(unit);
        _context.Units.Update(unitEntity);
        await _context.SaveChangesAsync();
        ClearTracking();
    }

    public async Task<bool> IsUse(Guid id)
    {
        var isUse = await _context.ReceiptResources.AnyAsync(rr => rr.Unit.Id == id);
        return isUse;
    }

    public async Task Delete(Guid id)
    {
        var unitEntity = await _context.Units.FindAsync(id);
        if (unitEntity == null)
            return;

        _context.Units.Remove(unitEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Unit>> Find(List<Guid>? ids = null)
    {
        var query = _context.Units;

        if (ids != null && ids.Any())
            query.Where(u => ids.Contains(u.Id));

        var unitEntities = await query.ToListAsync();
        
        ClearTracking();
        return _mapper.ProjectTo<Unit>(unitEntities.AsQueryable()).ToList();
    }

    void ClearTracking() =>
        _context.ChangeTracker.Clear();
}