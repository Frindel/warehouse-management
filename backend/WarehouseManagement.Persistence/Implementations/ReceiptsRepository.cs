using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Persistence.Implementations;

public class ReceiptsRepository : IReceiptsRepository
{
    private readonly DataContext _context;

    public ReceiptsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Receipt?> TryGet(string number)
    {
        return await _context.Receipts
            .Include(r => r.Resources)
                .ThenInclude(rr => rr.Resource)
            .Include(r => r.Resources)
                .ThenInclude(rr => rr.Unit)
            .FirstOrDefaultAsync(r => r.Number == number);
    }

    public async Task<Receipt?> TryGet(Guid id)
    {
        return await _context.Receipts
            .Include(r => r.Resources)
                .ThenInclude(rr => rr.Resource)
            .Include(r => r.Resources)
                .ThenInclude(rr => rr.Unit)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Guid> Create(Receipt document)
    {
        document.Id = Guid.NewGuid();
        foreach (var resource in document.Resources)
        {
            resource.Id = Guid.NewGuid();
        }

        _context.Receipts.Add(document);
        await _context.SaveChangesAsync();
        return document.Id;
    }

    public async Task Delete(Guid id)
    {
        var receipt = await _context.Receipts.FindAsync(id);
        if (receipt != null)
        {
            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Update(Receipt document)
    {
        _context.Receipts.Update(document);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Receipt>> Find(List<string>? number = null, (DateOnly begin, DateOnly end)? period = null, List<Guid>? unitIds = null, List<Guid>? productIds = null)
    {
        var query = _context.Receipts
            .Include(r => r.Resources)
                .ThenInclude(rr => rr.Resource)
            .Include(r => r.Resources)
                .ThenInclude(rr => rr.Unit)
            .AsQueryable();

        if (number != null && number.Any())
            query = query.Where(r => number.Contains(r.Number));

        if (period.HasValue)
            query = query.Where(r => r.Date >= period.Value.begin && r.Date <= period.Value.end);

        if (unitIds != null && unitIds.Any())
            query = query.Where(r => r.Resources.Any(rr => unitIds.Contains(rr.Unit.Id)));

        if (productIds != null && productIds.Any())
            query = query.Where(r => r.Resources.Any(rr => productIds.Contains(rr.Resource.Id)));

        return await query.ToListAsync();
    }
}