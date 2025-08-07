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
        var command = new ChangeUnitCommand()
        {
            Id = Guid.NewGuid(),
            Name = "kg",
            IsArchived = false
        };

        var targetUnit = new Unit(command.Id, command.Name, command.IsArchived);
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(new Unit());
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.Update(It.IsAny<Unit>()));
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(targetUnit);

        // Act
        var changedUnit = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(changedUnit, Is.EqualTo(targetUnit));
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
            .Setup(ur => ur.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(null as Unit);

        // Act / assert
        Assert.ThrowsAsync<NotFoundException>(()=> _handler.Service.Handle(command, CancellationToken.None));
    }
    
}