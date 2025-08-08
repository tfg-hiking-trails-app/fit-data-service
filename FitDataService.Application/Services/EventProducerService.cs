using System.Text;
using System.Text.Json;
using Common.Domain.Interfaces.Messaging;
using FitDataService.Application.DTOs.Messaging;
using FitDataService.Application.Interfaces;

namespace FitDataService.Application.Services;

public class EventProducerService : IEventProducerService
{
    private readonly IRabbitMqQueueProducer _queueProducer;

    public EventProducerService(
        IRabbitMqQueueProducer queueProducer)
    {
        _queueProducer = queueProducer;
    }
    
    public async Task Send(FitFileDataEntityDto fitFileDataEntityDto)
    {
        string message = JsonSerializer.Serialize(fitFileDataEntityDto);
        
        await _queueProducer.BasicPublishAsync(Encoding.UTF8.GetBytes(message));
    }
    
}