using Moq;
using ServiceMock;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Receipts.Commands;
using WarehouseManagement.Domain;

namespace WarehouseManagement.Tests.Application.Receipts;

[TestFixture]
public class ChangeReceiptTests
{
    private ServiceMock<ChangeReceiptCommandHandler> _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new();
    }

    [Test]
    public async Task SuccessfullyChangesReceipt()
    {
        // Arrange
        var receiptId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var unitId = Guid.NewGuid();
        var receiptResourceId = Guid.NewGuid();

        var resource = new Resource(resourceId, "Steel");
        var unit = new Unit(unitId, "kg");

        var existingReceipt = new Receipt("OLD001", DateOnly.FromDateTime(DateTime.Today), new List<ReceiptResource>
        {
            new ReceiptResource(5, resource, unit) { Id = receiptResourceId }
        }) { Id = receiptId };

        var command = new ChangeReceiptCommand
        {
            Id = receiptId,
            Number = "NEW001",
            Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            Resources = new List<ChangingReceiptResource>
            {
                new ChangingReceiptResource
                {
                    Id = receiptResourceId,
                    ResourceId = resourceId,
                    UnitId = unitId,
                    Quantity = 10
                }
            }
        };

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.TryGet(command.Number))
            .ReturnsAsync(existingReceipt); // Existing receipt found

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.TryGet(command.Id))
            .ReturnsAsync(existingReceipt); // After update

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(r => r.TryGet(resourceId))
            .ReturnsAsync(resource);

        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(u => u.TryGet(unitId))
            .ReturnsAsync(unit);

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.Update(It.IsAny<Receipt>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Number, Is.EqualTo(command.Number));
        Assert.That(result.Date, Is.EqualTo(command.Date));
        Assert.That(result.Resources.Count, Is.EqualTo(1));
        Assert.That(result.Resources[0].Quantity, Is.EqualTo(10));
    }

    [Test]
    public void ThrowsAlreadyExistsException_WhenReceiptNotFoundByNumber()
    {
        // Arrange
        var command = new ChangeReceiptCommand
        {
            Id = Guid.NewGuid(),
            Number = "R002",
            Date = DateOnly.FromDateTime(DateTime.Today),
            Resources = new()
        };

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.TryGet(command.Number))
            .ReturnsAsync((Receipt)null!); // Receipt not found

        // Act & Assert
        Assert.ThrowsAsync<AlreadyExistsException>(() => _handler.Service.Handle(command, CancellationToken.None));
    }

    [Test]
    public void ThrowsNotFoundException_WhenResourceNotFound()
    {
        // Arrange
        var receiptId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var unitId = Guid.NewGuid();

        var existingReceipt = new Receipt("R001", DateOnly.FromDateTime(DateTime.Today), new List<ReceiptResource>()) { Id = receiptId };

        var command = new ChangeReceiptCommand
        {
            Id = receiptId,
            Number = "R001",
            Date = DateOnly.FromDateTime(DateTime.Today),
            Resources = new List<ChangingReceiptResource>
            {
                new ChangingReceiptResource
                {
                    ResourceId = resourceId,
                    UnitId = unitId,
                    Quantity = 5
                }
            }
        };

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.TryGet(command.Number))
            .ReturnsAsync(existingReceipt);

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(r => r.TryGet(resourceId))
            .ReturnsAsync((Resource)null!); // Resource not found

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() => _handler.Service.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain(resourceId.ToString()));
    }

    [Test]
    public void ThrowsNotFoundException_WhenUnitNotFound()
    {
        // Arrange
        var receiptId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var unitId = Guid.NewGuid();

        var resource = new Resource(resourceId, "iron");

        var existingReceipt = new Receipt("R001", DateOnly.FromDateTime(DateTime.Today), new List<ReceiptResource>()) { Id = receiptId };

        var command = new ChangeReceiptCommand
        {
            Id = receiptId,
            Number = "R001",
            Date = DateOnly.FromDateTime(DateTime.Today),
            Resources = new List<ChangingReceiptResource>
            {
                new ChangingReceiptResource
                {
                    ResourceId = resourceId,
                    UnitId = unitId,
                    Quantity = 5
                }
            }
        };

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.TryGet(command.Number))
            .ReturnsAsync(existingReceipt);

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(r => r.TryGet(resourceId))
            .ReturnsAsync(resource);

        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(u => u.TryGet(unitId))
            .ReturnsAsync((Unit)null!); // Unit not found

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() => _handler.Service.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain(unitId.ToString()));
    }

    [Test]
    public void ThrowsNotFoundException_WhenReceiptResourceIdNotFoundInExistingResources()
    {
        // Arrange
        var receiptId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var unitId = Guid.NewGuid();
        var missingResourceId = Guid.NewGuid();

        var resource = new Resource(resourceId, "Steel");
        var unit = new Unit(unitId, "kg");

        var existingReceipt = new Receipt("R001", DateOnly.FromDateTime(DateTime.Today), new List<ReceiptResource>
        {
            new ReceiptResource(5, resource, unit) { Id = Guid.NewGuid() } // Not matching ID
        }) { Id = receiptId };

        var command = new ChangeReceiptCommand
        {
            Id = receiptId,
            Number = "R001",
            Date = DateOnly.FromDateTime(DateTime.Today),
            Resources = new List<ChangingReceiptResource>
            {
                new ChangingReceiptResource
                {
                    Id = missingResourceId,
                    ResourceId = resourceId,
                    UnitId = unitId,
                    Quantity = 10
                }
            }
        };

        _handler.GetParameterMock<IReceiptsRepository>()
            .Setup(r => r.TryGet(command.Number))
            .ReturnsAsync(existingReceipt);

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(r => r.TryGet(resourceId))
            .ReturnsAsync(resource);

        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(u => u.TryGet(unitId))
            .ReturnsAsync(unit);

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() => _handler.Service.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain(missingResourceId.ToString()));
    }
}
