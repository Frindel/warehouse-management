using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ServiceMock;
using WarehouseManagement.Domain;
using WarehouseManagement.Persistence;
using WarehouseManagement.Persistence.Implementations;

namespace WarehouseManagement.Tests.Persistence.Units;

[TestFixture]
public class CreateTests
{
    private DataContext _context;
    private ServiceMock<UnitsRepository> _repository;

    [SetUp]
    public void Setup()
    {
        _context = CreateSqliteInMemoryContext();
        _repository = new ServiceMock<UnitsRepository>(options => { options.SetParameter(_context); });
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
        context.Units.Add(new Unit("kg"));
        context.SaveChanges();

        return context;
    }

    #endregion

    [Test]
    public async Task SuccessfulyCreatingUnit()
    {
        // Arrange
        var newUnit = new Unit("g");

        // Act
        var unitId = await _repository.Service.Create(newUnit);

        // Assert
        var savedUnit = _context.Units.FirstOrDefault(u => u.Id == unitId && u.Name == newUnit.Name);
        Assert.That(savedUnit, Is.Not.Null);
        Assert.That(savedUnit.Name, Is.EqualTo(newUnit.Name));
    }

    [Test]
    public void ThrowsUnitWithNameAlreadyExists()
    {
        // Arrange
        var newUnit = new Unit("kg");

        // Act / assert
        Assert.ThrowsAsync<DbUpdateException>(() => _repository.Service.Create(newUnit));
    }
}