using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Units.Commands;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Units;

[TestFixture]
public class CreateUnitTests
{
    private ServiceMock<CreateUnitCommandHandler> _handler;
    
    [SetUp]
    public void Setup()
    {
        _handler = new();
    }
    
    [Test]
    public async Task SuccessfullyCreateUnit()
    {
        // Arrange
        var command = new CreateUnitCommand()
        {
            Name = "kg"
        };

        var targetUnit = new Unit(Guid.NewGuid(), command.Name);
        var storage = new List<Unit>();
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.TryGet(It.IsAny<string>()))
            .ReturnsAsync(null as Unit);
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.Create(It.IsAny<Unit>()))
            .ReturnsAsync(targetUnit.Id);
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(targetUnit);
        
        // Act
        var savedUnit = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(savedUnit, Is.EqualTo(targetUnit));
    }
    
    [Test]
    public void ThrowsUnitAlreadyExistsException()
    {
        // Arrange
        var command = new CreateUnitCommand()
        {
            Name = "kg"
        };
        
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.TryGet(It.IsAny<string>()))
            .ReturnsAsync(new Unit());
        
        // Act / assert
        Assert.ThrowsAsync<AlreadyExistsException>(() => _handler.Service.Handle(command, CancellationToken.None));
    }
    
}