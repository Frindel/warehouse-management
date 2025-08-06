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

        var savedUnitId = Guid.NewGuid();
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.IsExist(It.IsAny<string>()))
            .ReturnsAsync(false);
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.Create(It.IsAny<Unit>()))
            .ReturnsAsync(savedUnitId);
        
        // Act
        var unitId = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(unitId, Is.EqualTo(savedUnitId), "Не ожидаемый идентификатор единицы измерения");
    }
    
    // запись с данным именем уже существует
    [Test]
    public void ThrowsUnitAlreadyExistsException()
    {
        // Arrange
        var command = new CreateUnitCommand()
        {
            Name = "kg"
        };
        
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.IsExist(It.IsAny<string>()))
            .ReturnsAsync(true);
        
        // Act / assert
        Assert.ThrowsAsync<AlreadyExistsException>(() => _handler.Service.Handle(command, CancellationToken.None));
    }
    
}