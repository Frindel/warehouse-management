using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ServiceMock;
using WarehouseManagement.Domain;
using WarehouseManagement.Persistence;
using WarehouseManagement.Persistence.Implementations;

namespace WarehouseManagement.Tests.Persistence.Resources;

[TestFixture]
public class CreateTests
{
    private DataContext _context;
    private ServiceMock<ResourcesRepository> _repository;

    [SetUp]
    public void Setup()
    {
        _context = CreateSqliteInMemoryContext();
        _repository = new ServiceMock<ResourcesRepository>(options => { options.SetParameter(_context); });
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
        context.Resources.Add(new Resource("iron"));
        context.SaveChanges();

        return context;
    }

    #endregion

    [Test]
    public async Task SuccessfulyCreatingUnit()
    {
        // Arrange
        var newResource = new Resource("cuprum");

        // Act
        var resourceId = await _repository.Service.Create(newResource);

        // Assert
        var savedUnit = _context.Resources.FirstOrDefault(u => u.Id == resourceId && u.Name == newResource.Name);
        Assert.That(savedUnit, Is.Not.Null);
        Assert.That(savedUnit.Name, Is.EqualTo(newResource.Name));
    }

    [Test]
    public void ThrowsUnitWithNameAlreadyExists()
    {
        // Arrange
        var newUnit = new Resource("iron");

        // Act / assert
        Assert.ThrowsAsync<DbUpdateException>(() => _repository.Service.Create(newUnit));
    }
}