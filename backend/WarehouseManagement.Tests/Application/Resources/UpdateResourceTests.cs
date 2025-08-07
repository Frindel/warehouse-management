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
        var resourceId = Guid.NewGuid();
        var command = new ChangeResourceCommand()
        {
            Id = resourceId,
            Name = "iron",
            IsArchived = false
        };

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(ur => ur.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(new Resource());
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(ur => ur.Update(It.IsAny<Resource>()));

        // Act
        var changedResourceId = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(changedResourceId, Is.EqualTo(resourceId), "Не ожидаемый идентификатор ресурса");
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
            .Setup(ur => ur.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(null as Resource);

        // Act / assert
        Assert.ThrowsAsync<NotFoundException>(()=> _handler.Service.Handle(command, CancellationToken.None));
    }
    
}