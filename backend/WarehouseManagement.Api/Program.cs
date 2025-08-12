using WarehouseManagement.Api.Middleware;
using WarehouseManagement.Application;
using WarehouseManagement.Persistence;

namespace WarehouseManagement.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.OnPersistence(configuration);
        builder.Services.OnApplication();
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseMiddleware<ExceptionsHandlerMiddleware>();
        app.MapControllers();
        app.Run();
    }
}