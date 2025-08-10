using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.Application.Common.Behaviors;
using WarehouseManagement.Application.Receipts.Helpers;

namespace WarehouseManagement.Application;

public static class DependencyInjection
{
    public static void OnApplication(this IServiceCollection services)
    {
        services.AddTransient<ReceiptsHelper>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}