using FitDataService.Application.Interfaces;
using FitDataService.Application.Services;

namespace FitDataService.Worker.Extensions;

public static class ServiceCollectionExtension
{
    public static void ServiceCollectionConfiguration(this IServiceCollection services)
    {
        services.AddHostedServices();
        services.AddServices();
    }

    private static void AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<Worker>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEventConsumerService, EventConsumerService>();
    }
    
}