using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Domain;
using WarehouseManagement.Persistence.Entities;

namespace WarehouseManagement.Persistence.Implementations;

public class ResourcesRepository : IResourcesRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ResourcesRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Guid> Create(Resource unit)
    {
        var resourceEntity = _mapper.Map<ResourceEntity>(unit);
        _context.Resources.Add(resourceEntity);
        await _context.SaveChangesAsync();
        ClearTracking();
        return resourceEntity.Id;
    }

    public async Task<Resource?> TryGet(string name)
    {
        var resourceEntity = await _context.Resources.FirstOrDefaultAsync(u => u.Name == name);
        if (resourceEntity == null)
            return null;
        ClearTracking();
        return _mapper.Map<Resource>(resourceEntity);
    }

    public async Task<Resource?> TryGet(Guid id)
    {
        var resourceEntity = await _context.Resources.FindAsync(id);
        ClearTracking();
        return _mapper.Map<Resource>(resourceEntity);
    }

    public async Task Update(Resource unit)
    {
        var resourceEntity = _mapper.Map<ResourceEntity>(unit);
        _context.Resources.Update(resourceEntity);
        await _context.SaveChangesAsync();
        ClearTracking();
    }

    public async Task<bool> IsUse(Guid id)
    {
        var isUse = await _context.ReceiptResources.AnyAsync(rr => rr.ResourceId == id);
        return isUse;
    }

    public async Task Delete(Guid id)
    {
        var resourceEntity = await _context.Resources.FindAsync(id);
        if (resourceEntity != null)
        {
            _context.Resources.Remove(resourceEntity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Resource>> Find(List<Guid>? ids = null)
    {
        var query = _context.Resources;

        if (ids != null && ids.Any())
            query.Where(u => ids.Contains(u.Id));

        var resourceEntity = await query.ToListAsync();
        
        ClearTracking();
        return _mapper.ProjectTo<Resource>(resourceEntity.AsQueryable()).ToList();
    }
    
    void ClearTracking() =>
        _context.ChangeTracker.Clear();
}