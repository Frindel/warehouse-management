using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Persistence.Implementations;

public class UnitsRepository : IUnitsRepository
{
    private readonly DataContext _context;

    public UnitsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> Create(Unit unit)
    {
        unit.Id = Guid.NewGuid();
        _context.Units.Add(unit);
        await _context.SaveChangesAsync();
        return unit.Id;
    }

    public async Task<Unit?> TryGet(string name)
    {
        return await _context.Units.FirstOrDefaultAsync(u => u.Name == name);
    }

    public async Task<Unit?> TryGet(Guid id)
    {
        return await _context.Units.FindAsync(id);
    }

    public async Task Update(Unit unit)
    {
        _context.Units.Update(unit);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsUse(Guid id)
    {
        return await _context.ReceiptResources.AnyAsync(rr => rr.Unit.Id == id);
    }

    public async Task Delete(Guid id)
    {
        var unit = await _context.Units.FindAsync(id);
        if (unit != null)
        {
            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Unit>> Find(List<Guid>? ids = null)
    {
        if (ids == null || !ids.Any())
            return await _context.Units.ToListAsync();

        return await _context.Units.Where(u => ids.Contains(u.Id)).ToListAsync();
    }
}
