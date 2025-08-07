using MediatR;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;

namespace WarehouseManagement.Application.Resources.Commands;

public record DeleteResourceCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}

public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand, Guid>
{
    private readonly IResourcesRepository _resources;
    
    public DeleteResourceCommandHandler(IResourcesRepository resources)
    {
        _resources = resources;
    }
    
    public async Task<Guid> Handle(DeleteResourceCommand command, CancellationToken cancellationToken)
    {
        if (await _resources.TryGet(command.Id) == null)
            throw new NotFoundException($"Resource with name {command.Id} not exists.");
        
        if (await _resources.IsUse(command.Id))
            throw new InUseException($"Resource with id {command.Id} is use.");

        await _resources.Delete(command.Id);
        return command.Id;
    }
}