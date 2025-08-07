using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Resources.Queries;

public record GetResourceQuery : IRequest<Resource>
{
    public Guid Id { get; set; }
}

public class GetResourceQueryHandler : IRequestHandler<GetResourceQuery, Resource>
{
    private IResourcesRepository _resources;

    public GetResourceQueryHandler(IResourcesRepository resources)
    {
        _resources = resources;
    }

    public async Task<Resource> Handle(GetResourceQuery query, CancellationToken cancellationToken)
    {
        var receipt = await _resources.TryGet(query.Id);
        return receipt ?? throw new NotFoundException($"Unit with id {query.Id} not found");
    }
}