using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Resources.Commands;

public record ChangeResourceCommand : IRequest<Resource>
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public bool IsArchived { get; set; }
}

public class ChangeResourceCommandHandler : IRequestHandler<ChangeResourceCommand, Resource>
{
    private readonly IResourcesRepository _resources;
    
    public ChangeResourceCommandHandler(IResourcesRepository resources)
    {
        _resources = resources;
    }
    
    public async Task<Resource> Handle(ChangeResourceCommand command, CancellationToken cancellationToken)
    {
        if (await _resources.TryGet(command.Id) == null)
            throw new NotFoundException($"Resource with id {command.Id} not exists.");
        
        await _resources.Update(new Resource(command.Id, command.Name, command.IsArchived));
        return (await _resources.TryGet(command.Id)!)!;
    }
}