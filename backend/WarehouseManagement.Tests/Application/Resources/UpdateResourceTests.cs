using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Resources.Commands;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Resources;

[TestFixture]
public class UpdateResourceTests
{
    private ServiceMock<ChangeResourceCommandHandler> _handler;
    
    [SetUp]
    public void Setup()
    {
        _handler = new ();
    }
    
    [Test]
    public async Task SuccessfullyUpdateResource()
    {
        // Arrange
        var command = new ChangeResourceCommand()
        {
            Id = Guid.NewGuid(),
            Name = "iron",
            IsArchived = false
        };

        var targetResource = new Resource(command.Id, command.Name, command.IsArchived);
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(new Resource());
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.Update(It.IsAny<Resource>()));
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(targetResource);

        // Act
        var changedResource = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(changedResource, Is.EqualTo(targetResource));
    }
    
    [Test]
    public void ThrowsResourceNotFoundException()
    {
        // Arrange
        var command = new ChangeResourceCommand()
        {
            Id = Guid.NewGuid(),
            Name = "iron",
            IsArchived = false
        };
        
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(null as Resource);

        // Act / assert
        Assert.ThrowsAsync<NotFoundException>(()=> _handler.Service.Handle(command, CancellationToken.None));
    }
    
}