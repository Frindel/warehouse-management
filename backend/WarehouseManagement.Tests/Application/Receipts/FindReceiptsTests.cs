using Moq;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Receipts.Queries;
using WarehouseManagement.Domain;
using ServiceMock;

namespace WarehouseManagement.Tests.Application.Receipts;

[TestFixture]
public class FindReceiptsTests
{
    private ServiceMock<FindReceiptsQueryHandler> _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new ServiceMock<FindReceiptsQueryHandler>();
    }

    [Test]
    public async Task SuccessfullyFindReceipts()
    {
        // Arrange
        var query = new FindReceiptsQuery
        {
            Numbers = new List<string> { "R001", "R002" },
            From = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)),
            To = DateOnly.FromDateTime(DateTime.Now),
            UnitsId = new List<Guid> { Guid.NewGuid() },
            ProductIds = new List<Guid> { Guid.NewGuid() }
        };

        var expectedReceipts = new List<Receipt>()
        {
            new Receipt("R001", DateOnly.FromDateTime(DateTime.Now), new List<ReceiptResource>()),
            new Receipt("R002", DateOnly.FromDateTime(DateTime.Now), new List<ReceiptResource>())
        };

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.Find(It.IsAny<List<string>>(), It.IsAny<(DateOnly, DateOnly)?>(), It.IsAny<List<Guid>>(),
                It.IsAny<List<Guid>>()))
            .ReturnsAsync(expectedReceipts);

        // Act
        var result = await _handler.Service.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(expectedReceipts));
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task ReturnsEmptyListWhenNoReceiptsFound()
    {
        // Arrange
        var query = new FindReceiptsQuery
        {
            Numbers = new List<string> { "R003" },
            From = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
            To = DateOnly.FromDateTime(DateTime.Now),
            UnitsId = new List<Guid> { Guid.NewGuid() },
            ProductIds = new List<Guid> { Guid.NewGuid() }
        };

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.Find(It.IsAny<List<string>>(), It.IsAny<(DateOnly, DateOnly)?>(), It.IsAny<List<Guid>>(),
                It.IsAny<List<Guid>>()))
            .ReturnsAsync(new List<Receipt>());

        // Act
        var result = await _handler.Service.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task HandlesNullParameters()
    {
        // Arrange
        var query = new FindReceiptsQuery
        {
            Numbers = null,
            From = null,
            To = null,
            UnitsId = null,
            ProductIds = null
        };

        var expectedReceipts = new List<Receipt>
        {
            new Receipt("R001", DateOnly.FromDateTime(DateTime.Now), new List<ReceiptResource>())
        };

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.Find(null, null, null, null))
            .ReturnsAsync(expectedReceipts);

        // Act
        var result = await _handler.Service.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(expectedReceipts));
        Assert.That(result.Count, Is.EqualTo(1));
    }
}