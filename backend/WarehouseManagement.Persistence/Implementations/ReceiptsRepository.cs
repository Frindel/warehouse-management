using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Domain;
using WarehouseManagement.Persistence.Entities;

namespace WarehouseManagement.Persistence.Implementations;

public class ReceiptsRepository : IReceiptsRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ReceiptsRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Receipt?> TryGet(string number)
    {
        var receiptEntity = await _context.Receipts
            .Include(r => r.Resources)
            .ThenInclude(rr => rr.Resource)
            .Include(r => r.Resources)
            .ThenInclude(rr => rr.Unit)
            .FirstOrDefaultAsync(r => r.Number == number);

        ClearTracking();
        return _mapper.Map<Receipt>(receiptEntity);
    }

    public async Task<Receipt?> TryGet(Guid id)
    {
        var receiptEntity = await _context.Receipts
            .Include(r => r.Resources)
            .ThenInclude(rr => rr.Resource)
            .Include(r => r.Resources)
            .ThenInclude(rr => rr.Unit)
            .FirstOrDefaultAsync(r => r.Id == id);

        ClearTracking();
        return _mapper.Map<Receipt>(receiptEntity);
    }

    public async Task<Guid> Create(Receipt receipt)
    {
        var receiptEntity = _mapper.Map<ReceiptDocumentEntity>(receipt);

        // итерация по ресурсам
        foreach (var resource in receiptEntity.Resources)
        {
            _context.Attach(resource.Resource);
            _context.Attach(resource.Unit);
        }

        _context.Receipts.Add(receiptEntity);
        await _context.SaveChangesAsync();

        ClearTracking();
        return receiptEntity.Id;
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

    public async Task Update(Receipt receipt)
    {
        var receiptEntity = _mapper.Map<ReceiptDocumentEntity>(receipt);
        var savedReceipt = await _context.Receipts
            .Include(r => r.Resources)
            .FirstAsync(r => r.Id == receiptEntity.Id);

        _context.Entry(savedReceipt).CurrentValues.SetValues(receiptEntity);
        DeleteMissingResources(savedReceipt.Resources, receiptEntity.Resources);
        AddOrUpdateResources(savedReceipt.Resources, receiptEntity.Resources);

        await _context.SaveChangesAsync();
        ClearTracking();
    }

    void DeleteMissingResources(List<ReceiptResourceEntity> savedResources,
        List<ReceiptResourceEntity> updatedResources)
    {
        var incomingResourceIds = updatedResources
            .Where(r => r.Id != Guid.Empty)
            .Select(r => r.Id)
            .ToList();

        var resourcesToRemove = savedResources
            .Where(r => !incomingResourceIds.Contains(r.Id))
            .ToList();
        foreach (var resource in resourcesToRemove)
            _context.ReceiptResources.Remove(resource);
    }

    void AddOrUpdateResources(List<ReceiptResourceEntity> savedResources,
        List<ReceiptResourceEntity> updatedResources)
    {
        foreach (var resource in updatedResources)
        {
            if (resource.Id == Guid.Empty)
                savedResources.Add(resource);
            else
            {
                var existingResource = savedResources
                    .FirstOrDefault(r => r.Id == resource.Id);
                if (existingResource != null)
                    _context.Entry(existingResource).CurrentValues.SetValues(resource);
            }
        }
    }

    public async Task<List<Receipt>> Find(List<string>? number = null, (DateOnly begin, DateOnly end)? period = null,
        List<Guid>? unitIds = null, List<Guid>? productIds = null)
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

        var receiptEntities = await query.ToListAsync();

        ClearTracking();
        return _mapper.ProjectTo<Receipt>(receiptEntities.AsQueryable()).ToList();
    }

    void ClearTracking() =>
        _context.ChangeTracker.Clear();
}