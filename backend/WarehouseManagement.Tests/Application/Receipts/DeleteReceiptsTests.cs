using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Receipts.Commands;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Receipts;

public class DeleteReceiptsTests
{
    private ServiceMock<DeleteReceiptCommandHandler> _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new ServiceMock<DeleteReceiptCommandHandler>();
    }

    [Test]
    public async Task SuccessfullyDeleteReceipt()
    {
        // Arrange
        var receiptId = Guid.NewGuid();
        var command = new DeleteReceiptCommand { Id = receiptId };

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(new Receipt());

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.Delete(It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(receiptId), "Неожиданный идентификатор чека");
    }

    [Test]
    public void ThrowsReceiptNotFoundException()
    {
        // Arrange
        var command = new DeleteReceiptCommand { Id = Guid.NewGuid() };

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.TryGet(It.IsAny<Guid>()))
            .ReturnsAsync(null as Receipt);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Service.Handle(command, CancellationToken.None));
    }
}