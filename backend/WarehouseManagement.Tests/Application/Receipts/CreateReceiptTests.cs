using Moq;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Application.Common.Exceptions;
using WarehouseManagement.Application.Receipts.Commands;
using WarehouseManagement.Domain;
using ServiceMock;

namespace WarehouseManagement.Tests.Application.Receipts;

[TestFixture]
public class CreateReceiptTests
{
    private ServiceMock<CreateReceiptCommandHandler> _handler;

    [SetUp]
    public void Setup()
    {
        _handler = new();
    }

    [Test]
    public async Task SuccessfullyCreatesReceipt()
    {
        // Arrange
        var resourceId = Guid.NewGuid();
        var unitId = Guid.NewGuid();
        var receiptId = Guid.NewGuid();

        var command = new CreateReceiptCommand
        {
            Number = "R001",
            Date = DateOnly.FromDateTime(DateTime.Now),
            Resources = new List<CreatingReceiptResource>
            {
                new CreatingReceiptResource
                {
                    ResourceId = resourceId,
                    UnitId = unitId,
                    Quantity = 10
                }
            }
        };

        var resource = new Resource(resourceId, "iron");
        var unit = new Unit(unitId, "kg");
        var expectedReceipt = new Receipt(command.Number, command.Date, new List<ReceiptResource>
        {
            new ReceiptResource(10, resource, unit)
        });

        _handler.GetParameterMock<IReceiptDocumentsRepository>()
            .Setup(r => r.TryGet(command.Number))
            .ReturnsAsync((Receipt)null!);

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(r => r.TryGet(resourceId))
            .ReturnsAsync(resource);

        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(u => u.TryGet(unitId))
            .ReturnsAsync(unit);

        _handler.GetParameterMock<IReceiptDocumentsRepository>()
            .Setup(r => r.Create(It.IsAny<Receipt>()))
            .ReturnsAsync(receiptId);

        _handler.GetParameterMock<IReceiptDocumentsRepository>()
            .Setup(r => r.TryGet(receiptId))
            .ReturnsAsync(expectedReceipt);

        // Act
        var result = await _handler.Service.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(expectedReceipt));
    }

    [Test]
    public void ThrowsAlreadyExistsException_WhenReceiptNumberAlreadyExists()
    {
        // Arrange
        var command = new CreateReceiptCommand
        {
            Number = "R001",
            Date = DateOnly.FromDateTime(DateTime.Now),
        };

        _handler.GetParameterMock<IReceiptDocumentsRepository>()
            .Setup(r => r.TryGet(command.Number))
            .ReturnsAsync(new Receipt());

        // Act & Assert
        Assert.ThrowsAsync<AlreadyExistsException>(() => _handler.Service.Handle(command, CancellationToken.None));
    }

    [Test]
    public void ThrowsNotFoundException_WhenResourceNotFound()
    {
        // Arrange
        var resourceId = Guid.NewGuid();
        var unitId = Guid.NewGuid();

        var command = new CreateReceiptCommand
        {
            Number = "R001",
            Date = DateOnly.FromDateTime(DateTime.Now),
            Resources = new List<CreatingReceiptResource>
            {
                new CreatingReceiptResource
                {
                    ResourceId = resourceId,
                    UnitId = unitId,
                    Quantity = 3
                }
            }
        };

        _handler.GetParameterMock<IReceiptDocumentsRepository>()
            .Setup(r => r.TryGet(command.Number))
            .ReturnsAsync((Receipt)null!);

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(r => r.TryGet(resourceId))
            .ReturnsAsync((Resource)null!); // Not found

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() => _handler.Service.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain(resourceId.ToString()));
    }

    [Test]
    public void ThrowsNotFoundException_WhenUnitNotFound()
    {
        // Arrange
        var resourceId = Guid.NewGuid();
        var unitId = Guid.NewGuid();

        var command = new CreateReceiptCommand
        {
            Number = "R001",
            Date = DateOnly.FromDateTime(DateTime.Now),
            Resources = new List<CreatingReceiptResource>
            {
                new CreatingReceiptResource
                {
                    ResourceId = resourceId,
                    UnitId = unitId,
                    Quantity = 7
                }
            }
        };

        var resource = new Resource(resourceId, "iron");

        _handler.GetParameterMock<IReceiptDocumentsRepository>()
            .Setup(r => r.TryGet(command.Number))
            .ReturnsAsync((Receipt)null!);

        _handler.GetParameterMock<IResourcesRepository>()
            .Setup(r => r.TryGet(resourceId))
            .ReturnsAsync(resource);

        _handler.GetParameterMock<IUnitsRepository>()
            .Setup(u => u.TryGet(unitId))
            .ReturnsAsync((Unit)null!); // Not found

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() => _handler.Service.Handle(command, CancellationToken.None));
        Assert.That(ex!.Message, Does.Contain(unitId.ToString()));
    }
}