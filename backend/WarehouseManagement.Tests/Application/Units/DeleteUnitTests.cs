using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Units.Commands;

namespace WarehouseManagement.Tests.Application.Units;

[TestFixture]
public class DeleteUnitTests
{
    private ServiceMock<DeleteUnitCommandHandler> _handler;
    
    [SetUp]
    public void Setup()
    {
        _handler = new();
    }

    [Test]
    public async Task SuccessfullyDeleteUnit()
    {
        // Arrange
        var unitId = Guid.NewGuid();
        var command = new DeleteUnitCommand()
        {
            Id = unitId
        };
        
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.IsExist(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.IsUse(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.Delete(It.IsAny<Guid>()));
        
        // Act
        var deletedUnitId = await _handler.Service.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.That(deletedUnitId, Is.EqualTo(unitId), "Не ожидаемый идентификатор единицы измерения");
    }

    [Test]
    public void ThrowsUnitNotFoundException()
    {
        // Arrange
        var command = new DeleteUnitCommand()
        {
            Id = Guid.NewGuid()
        };
        
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.IsExist(It.IsAny<Guid>()))
            .ReturnsAsync(false);
        
        // Act / assert
        Assert.ThrowsAsync<NotFoundException>(()=> _handler.Service.Handle(command, CancellationToken.None));
    }

    [Test]
    public void ThrowsUnitIsUsedException()
    {
        // Arrange
        var command = new DeleteUnitCommand()
        {
            Id = Guid.NewGuid()
        };
        
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.IsExist(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.IsUse(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        
        // Act / assert
        Assert.ThrowsAsync<InUseException>(()=> _handler.Service.Handle(command, CancellationToken.None));
    }
}