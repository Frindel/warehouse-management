using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Persistence.Implementations;

public class ResourcesRepository : IResourcesRepository
{
    private readonly DataContext _context;

    public ResourcesRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> Create(Resource resource)
    {
        resource.Id = Guid.NewGuid();
        _context.Resources.Add(resource);
        await _context.SaveChangesAsync();
        return resource.Id;
    }

    public async Task<Resource?> TryGet(string name)
    {
        return await _context.Resources.FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<Resource?> TryGet(Guid id)
    {
        return await _context.Resources.FindAsync(id);
    }

    public async Task Update(Resource resource)
    {
        _context.Resources.Update(resource);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsUse(Guid id)
    {
        return await _context.ReceiptResources.AnyAsync(rr => rr.Resource.Id == id);
    }

    public async Task Delete(Guid id)
    {
        var resource = await _context.Resources.FindAsync(id);
        if (resource != null)
        {
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Resource>> Find(List<Guid>? ids = null)
    {
        if (ids == null || !ids.Any())
            return await _context.Resources.ToListAsync();

        return await _context.Resources.Where(r => ids.Contains(r.Id)).ToListAsync();
    }
}