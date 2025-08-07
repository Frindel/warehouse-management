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

        var targetResource = new Resource(Guid.NewGuid(), command.Name);
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.TryGet(It.IsAny<string>()))
            .ReturnsAsync(null as Resource);
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.Create(It.IsAny<Resource>()))
            .ReturnsAsync(targetResource.Id);
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(targetResource);

        // Act
        var savedResource = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(savedResource, Is.EqualTo(targetResource));
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
            .Setup(rr=> rr.TryGet(It.IsAny<string>()))
            .ReturnsAsync(new Resource());

        // Act / assert
        Assert.ThrowsAsync<AlreadyExistsException>(() => _handler.Service.Handle(command, CancellationToken.None));
    }
}