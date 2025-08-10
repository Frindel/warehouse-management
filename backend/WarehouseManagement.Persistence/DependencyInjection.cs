using System.Net.Mime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.Application.Common.Contracts;
using WarehouseManagement.Persistence.Implementations;

namespace WarehouseManagement.Persistence;

public static class DependencyInjection
{
    public static void OnPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AddDataContext(services, configuration);
        services.AddScoped<IReceiptsRepository, ReceiptsRepository>();
        services.AddScoped<IResourcesRepository, ResourcesRepository>();
        services.AddScoped<IUnitsRepository, UnitsRepository>();

        services.AddAutoMapper(config => config.AddMaps(typeof(DataContext).Assembly));
    }

    static void AddDataContext(IServiceCollection services, IConfiguration configuration)
    {
        string? dbConnectionString = configuration["connectionString"];
        if (dbConnectionString == null)
            throw new ArgumentNullException(nameof(dbConnectionString));

        services.AddNpgsql<DataContext>(dbConnectionString);
    }
}