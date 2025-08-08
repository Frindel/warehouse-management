using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain;
using WarehouseManagement.Persistence;
using WarehouseManagement.Persistence.Implementations;
using ServiceMock;

namespace WarehouseManagement.Tests.Persistence.Behavior;

[TestFixture]
public class FindTests
{
    private DataContext _context;
    private ServiceMock<ReceiptsRepository> _repository;

    [SetUp]
    public void Setup()
    {
        _context = CreateSqliteInMemoryContext();
        _repository = new ServiceMock<ReceiptsRepository>(options =>
        {
            options.SetParameter(_context);
        });
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    #region Helpers

    private DataContext CreateSqliteInMemoryContext()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlite(connection)
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureCreated();

        // Test data
        var unit1 = new Unit("kg");
        var unit2 = new Unit("l");
        var product1 = new Resource("iron");
        var product2 = new Resource("copper");

        var receipt1 = new Receipt("R-001", new DateOnly(2024, 01, 01));
        receipt1.Resources.Add(new ReceiptResource { Resource = product1, Unit = unit1 });

        var receipt2 = new Receipt("R-002", new DateOnly(2024, 03, 10));
        receipt2.Resources.Add(new ReceiptResource { Resource = product2, Unit = unit2 });

        context.Receipts.AddRange(receipt1, receipt2);
        context.SaveChanges();

        return context;
    }

    #endregion

    [Test]
    public async Task ReturnsReceiptsByNumber()
    {
        var result = await _repository.Service.Find(number: new List<string> { "R-001" });
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].Number, Is.EqualTo("R-001"));
    }

    [Test]
    public async Task ReturnsReceiptsByPeriod()
    {
        var result = await _repository.Service.Find(period: (new DateOnly(2024, 01, 01), new DateOnly(2024, 01, 02)));
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].Number, Is.EqualTo("R-001"));
    }

    [Test]
    public async Task ReturnsReceiptsByUnitId()
    {
        var unitId = _context.Units.First(u => u.Name == "kg").Id;
        var result = await _repository.Service.Find(unitIds: new List<Guid> { unitId });
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].Number, Is.EqualTo("R-001"));
    }

    [Test]
    public async Task ReturnsReceiptsByProductId()
    {
        var productId = _context.Resources.First(r => r.Name == "copper").Id;
        var result = await _repository.Service.Find(productIds: new List<Guid> { productId });
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].Number, Is.EqualTo("R-002"));
    }

    [Test]
    public async Task ReturnsReceiptsByCombinedFilters()
    {
        var unitId = _context.Units.First(u => u.Name == "l").Id;
        var productId = _context.Resources.First(r => r.Name == "copper").Id;
        var result = await _repository.Service.Find(
            number: new List<string> { "R-002" },
            period: (new DateOnly(2024, 03, 01), new DateOnly(2024, 03, 20)),
            unitIds: new List<Guid> { unitId },
            productIds: new List<Guid> { productId });

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].Number, Is.EqualTo("R-002"));
    }

    [Test]
    public async Task ReturnsEmptyIfNoMatch()
    {
        var result = await _repository.Service.Find(
            number: new List<string> { "R-999" },
            period: (new DateOnly(2025, 01, 01), new DateOnly(2025, 01, 02)),
            unitIds: new List<Guid> { Guid.NewGuid() },
            productIds: new List<Guid> { Guid.NewGuid() });

        Assert.That(result, Is.Empty);
    }
}