using Common.Domain.Interfaces.Messaging;
using FitDataService.Application.DTOs;
using FitDataService.Application.Interfaces;

namespace FitDataService.Application.Services;

public class EventConsumerService : IEventConsumerService
{
    private readonly IRabbitMqQueueConsumer _queueConsumer;

    public EventConsumerService(IRabbitMqQueueConsumer queueConsumer)
    {
        _queueConsumer = queueConsumer;
    }
    
    public async Task<string> Consume()
    {
        ActivityFileResponseDto file = await _queueConsumer.BasicConsumeAsync<ActivityFileResponseDto>();

        return file.FileName;
    }
}