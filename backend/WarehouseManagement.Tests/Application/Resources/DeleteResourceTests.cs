using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Resources.Commands;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Resources;

[TestFixture]
public class DeleteResourceTests
{
    private ServiceMock<DeleteResourceCommandHandler> _handler;
    
    [SetUp]
    public void Setup()
    {
        _handler = new();
    }

    [Test]
    public async Task SuccessfullyDeleteResource()
    {
        // Arrange
        var resourceId = Guid.NewGuid();
        var command = new DeleteResourceCommand()
        {
            Id = resourceId
        };
        
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(new Resource());

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.IsUse(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.Delete(It.IsAny<Guid>()));
        
        // Act
        var deletedResourceId = await _handler.Service.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.That(deletedResourceId, Is.EqualTo(resourceId), "Не ожидаемый идентификатор ресурса");
    }

    [Test]
    public void ThrowsResourceNotFoundException()
    {
        // Arrange
        var command = new DeleteResourceCommand()
        {
            Id = Guid.NewGuid()
        };
        
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(null as Resource);
        
        // Act / assert
        Assert.ThrowsAsync<NotFoundException>(()=> _handler.Service.Handle(command, CancellationToken.None));
    }

    [Test]
    public void ThrowsResourceIsUsedException()
    {
        // Arrange
        var command = new DeleteResourceCommand()
        {
            Id = Guid.NewGuid()
        };
        
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(new Resource());

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.IsUse(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        
        // Act / assert
        Assert.ThrowsAsync<InUseException>(()=> _handler.Service.Handle(command, CancellationToken.None));
    }
}