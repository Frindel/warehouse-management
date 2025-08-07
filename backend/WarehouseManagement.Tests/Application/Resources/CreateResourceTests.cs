using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Resources.Commands;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Resources;

[TestFixture]
public class CreateResourceTests
{
    private ServiceMock<CreateResourceCommandHandler> _handler;
    
    [SetUp]
    public void Setup()
    {
        _handler = new();
    }
    
    [Test]
    public async Task SuccessfullyCreateResource()
    {
        // Arrange
        var command = new CreateResourceCommand()
        {
            Name = "kg"
        };

        var savedResourceId = Guid.NewGuid();
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(ur => ur.TryGet(It.IsAny<string>()))
            .ReturnsAsync(null as Resource);
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(ur => ur.Create(It.IsAny<Resource>()))
            .ReturnsAsync(savedResourceId);
        
        // Act
        var ResourceId = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(ResourceId, Is.EqualTo(savedResourceId), "Не ожидаемый идентификатор ресурса");
    }
    
    [Test]
    public void ThrowsResourceAlreadyExistsException()
    {
        // Arrange
        var command = new CreateResourceCommand()
        {
            Name = "kg"
        };
        
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(ur => ur.TryGet(It.IsAny<string>()))
            .ReturnsAsync(new Resource());
        
        // Act / assert
        Assert.ThrowsAsync<AlreadyExistsException>(() => _handler.Service.Handle(command, CancellationToken.None));
    }
    
}