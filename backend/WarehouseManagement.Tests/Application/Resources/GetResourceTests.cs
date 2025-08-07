using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Resources.Queries;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Resources;

[TestFixture]
public class GetResourceTests
{
    private ServiceMock<GetResourceQueryHandler> _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new();
    }

    [Test]
    public async Task SucessfulyGettingUnit()
    {
        // Arrange
        var query = new GetResourceQuery()
        {
            Id = Guid.NewGuid(),
        };

        var targetResource = new Resource(query.Id, "kg");
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(targetResource);

        // Act
        var resource = await _handler.Service.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(resource, Is.EqualTo(targetResource));
    }
}