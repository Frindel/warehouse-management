using Moq;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Receipts.Queries;
using WarehouseManagement.Domain;
using ServiceMock;

namespace WarehouseManagement.Tests.Application.Receipts;

[TestFixture]
public class GetReceiptTests
{
    private ServiceMock<GetReceiptQueryHandler> _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new ServiceMock<GetReceiptQueryHandler>();
    }

    [Test]
    public async Task SuccessfullyGetReceipt()
    {
        // Arrange
        var query = new GetReceiptQuery() { Id = Guid.NewGuid() };
        var expectedReceipt = new Receipt("R001", DateOnly.FromDateTime(DateTime.Now), new List<ReceiptResource>());

        _handler.GetParameterMock<IReceiptDocumentsRepository>()
            .Setup(r => r.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(expectedReceipt);

        // Act
        var result = await _handler.Service.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(expectedReceipt));
    }

    [Test]
    public void ThrowsReceiptNotFoundException()
    {
        // Arrange
        var query = new GetReceiptQuery() { Id = Guid.NewGuid() };

        _handler.GetParameterMock<IReceiptDocumentsRepository>()
            .Setup(r => r.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(null as Receipt);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Service.Handle(query, CancellationToken.None));
    }
}