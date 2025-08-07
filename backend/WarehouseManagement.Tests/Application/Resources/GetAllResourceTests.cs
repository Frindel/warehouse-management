using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Resources.Queries;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Resources;

[TestFixture]
public class GetAllResourceTests
{
    private ServiceMock<GetAllResourcesQueryHandler> _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new();
    }

    [Test]
    public async Task SuccessfulGettingAllResources()
    {
        // Arrange
        var query = new GetAllResourcesQuery();

        var savedResources = new List<Resource>
        {
            new Resource(Guid.NewGuid(), "iron"),
            new Resource(Guid.NewGuid(), "copper")
        };
        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(rr => rr.Find(It.IsAny<List<Guid>?>()))
            .ReturnsAsync(savedResources);

        // Act
        var resources = await _handler.Service.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(resources, Is.EquivalentTo(savedResources));
    }
}