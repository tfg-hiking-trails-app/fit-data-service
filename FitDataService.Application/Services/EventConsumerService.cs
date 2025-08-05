using Common.Domain.Interfaces.Messaging;
using FitDataService.Application.DTOs;
using FitDataService.Application.Interfaces;
using FitDataService.Domain.Interfaces.Processors;

namespace FitDataService.Application.Services;

public class EventConsumerService : IEventConsumerService
{
    private readonly IRabbitMqQueueConsumer _queueConsumer;
    private readonly IActivityFileProcessorFactory _factory;

    private const string Folder = "/shared/data";

    public EventConsumerService(
        IRabbitMqQueueConsumer queueConsumer,
        IActivityFileProcessorFactory factory)
    {
        _queueConsumer = queueConsumer;
        _factory = factory;
    }
    
    public async Task Consume()
    {
        ActivityFileResponseDto file = await _queueConsumer.BasicConsumeAsync<ActivityFileResponseDto>();

        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        IActivityFileProcessor? activityFileProcessor = _factory.GetProcessor(extension);
        
        if (activityFileProcessor is null)
            throw new Exception($"No activity file processor found for {extension}");
        
        await activityFileProcessor.ReadActivityFile($"{Folder}/{file.FileName}");
    }
}