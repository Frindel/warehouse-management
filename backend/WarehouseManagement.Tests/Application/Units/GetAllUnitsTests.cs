using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Units.Queries;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Units;

[TestFixture]
public class GetAllUnitsTests
{
    private ServiceMock<GetAllUnitsQueryHandler> _handler;
    
    [SetUp]
    public void Setup()
    {
        _handler = new();
    }

    [Test]
    public async Task SuccessfulGettingAllUnits()
    {
        // Arrange
        var query = new GetAllUnitsQuery();

        var savedUnits = new List<Unit>
        {
            new Unit(Guid.NewGuid(), "kg"),
            new Unit(Guid.NewGuid(), "ml")
        };
        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(x => x.Find(It.IsAny<List<Guid>?>()))
            .ReturnsAsync(savedUnits);

        // Act
        var units = await _handler.Service.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(units, Is.EquivalentTo(savedUnits));
    }
}