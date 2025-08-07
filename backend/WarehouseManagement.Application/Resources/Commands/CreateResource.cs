using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Application.Resources.Commands;

public record CreateResourceCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
}

public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, Guid>
{
    private readonly IResourcesRepository _resources;
    
    public CreateResourceCommandHandler(IResourcesRepository resources)
    {
        _resources = resources;
    }
    
    public async Task<Guid> Handle(CreateResourceCommand command, CancellationToken cancellationToken)
    {
        if (await _resources.TryGet(command.Name) != null)
            throw new AlreadyExistsException($"Resource with name {command.Name} already exists.");

        Guid resourceId = await _resources.Create(new Resource(command.Name));
        return resourceId;
    }
}