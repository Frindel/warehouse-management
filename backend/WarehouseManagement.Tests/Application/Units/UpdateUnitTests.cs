using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Units.Commands;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Units;

[TestFixture]
public class UpdateUnitTests
{
    private ServiceMock<ChangeUnitCommandHandler> _handler;
    
    [SetUp]
    public void Setup()
    {
        _handler = new ();
    }
    
    [Test]
    public async Task SuccessfullyUpdateUnit()
    {
        // Arrange
        var unitId = Guid.NewGuid();
        var command = new ChangeUnitCommand()
        {
            Id = unitId,
            Name = "kg",
            IsArchived = false
        };

        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.IsExist(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.Update(It.IsAny<Unit>()));

        // Act
        var changedUnitId = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(changedUnitId, Is.EqualTo(unitId), "Не ожидаемый идентификатор единицы измерения");
    }
    
    [Test]
    public void ThrowsUnitNotFoundException()
    {
        // Arrange
        var command = new ChangeUnitCommand()
        {
            Id = Guid.NewGuid(),
            Name = "kg",
            IsArchived = false
        };
        
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.IsExist(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act / assert
        Assert.ThrowsAsync<NotFoundException>(()=> _handler.Service.Handle(command, CancellationToken.None));
    }
    
}