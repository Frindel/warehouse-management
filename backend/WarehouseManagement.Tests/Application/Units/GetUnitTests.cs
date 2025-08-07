using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Units.Queries;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Units;

[TestFixture]
public class GetUnitTests
{
    private ServiceMock<GetUnitQueryHandler> _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new();
    }

    [Test]
    public async Task SuccessfulyGettingUnit()
    {
        // Arrange
        var query = new GetUnitQuery()
        {
            Id = Guid.NewGuid(),
        };

        var targetUnit = new Unit(query.Id, "kg");
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(ur => ur.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(targetUnit);

        // Act
        var unit = await _handler.Service.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(unit, Is.EqualTo(targetUnit));
    }
}