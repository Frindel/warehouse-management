using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Resources.Queries;

public record GetAllResourcesQuery : IRequest<List<Resource>>;

public class GetAllResourcesQueryHandler : IRequestHandler<GetAllResourcesQuery, List<Resource>>
{
    private readonly IResourcesRepository _resources;
    
    public GetAllResourcesQueryHandler(IResourcesRepository resources)
    {
        _resources = resources;
    }
    
    public async Task<List<Resource>> Handle(GetAllResourcesQuery query, CancellationToken cancellationToken)
    {
        return await _resources.Find();
    }
}