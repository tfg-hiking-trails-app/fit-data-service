using Common.Domain.Interfaces.Messaging;
using Common.Infrastructure.Messaging.Configuration;
using FitDataService.Application.Interfaces;
using FitDataService.Application.Services;
using FitDataService.Infrastructure.Messaging.Consumer;
using FitDataService.Infrastructure.Messaging.Producer;

namespace FitDataService.Worker.Extensions;

public static class ServiceCollectionExtension
{
    public static void ServiceCollectionConfiguration(this IServiceCollection services)
    {
        services.AddHostedServices();
        services.AddServices();
        services.AddRabbitMq();
    }

    private static void AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<Worker>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEventConsumerService, EventConsumerService>();
    }
    
    private static void AddRabbitMq(this IServiceCollection services)
    {
        // Providers
        services.AddScoped<IRabbitMqConnectionProvider, RabbitMqConnectionProvider>();
        services.AddScoped<IRabbitMqChannelProvider, RabbitMqChannelProvider>();
        services.AddScoped<IRabbitMqQueueProvider, RabbitMqQueueProvider>();
        
        // Processors
        services.AddScoped<IRabbitMqQueueProducer, RabbitMqQueueProducer>();
        services.AddScoped<IRabbitMqQueueConsumer, RabbitMqQueueConsumer>();
    }
    
}