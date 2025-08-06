using Common.Domain.Interfaces.Messaging;
using Common.Infrastructure.Messaging.Configuration;
using FitDataService.Application.Interfaces;
using FitDataService.Application.Services;
using FitDataService.Domain.Interfaces;
using FitDataService.Domain.Interfaces.Processors;
using FitDataService.Infrastructure.Data;
using FitDataService.Infrastructure.Data.Configurations.Mapping;
using FitDataService.Infrastructure.Data.Repositories;
using FitDataService.Infrastructure.Messaging.Consumer;
using FitDataService.Infrastructure.Messaging.Producer;
using FitDataService.Infrastructure.Processors;

namespace FitDataService.Worker.Extensions;

public static class ServiceCollectionExtension
{
    public static void ServiceCollectionConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper();
        
        services.AddRabbitMq();
        
        services.AddHostedServices();
        
        services.AddServices();
        
        services.AddRepositories();
    }

    private static void AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<Worker>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        // Processors
        services.AddScoped<IActivityFileProcessor, FitFileProcessor>();
        
        // Factories
        services.AddScoped<IActivityFileProcessorFactory, ActivityFileProcessorFactory>();
        
        // Services
        services.AddScoped<IEventConsumerService, EventConsumerService>();
        services.AddScoped<IEventProducerService, EventProducerService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<MongoDbContext>();
        
        services.AddScoped<IFitFileDataRepository, FitFileDataRepository>();
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
    
    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(FitFileDataProfile).Assembly);
    }
    
}