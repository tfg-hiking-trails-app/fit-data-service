using AutoMapper;
using Common.Domain.Interfaces.Messaging;
using FitDataService.Application.DTOs;
using FitDataService.Application.DTOs.Messaging;
using FitDataService.Application.Interfaces;
using FitDataService.Domain.Interfaces.Processors;
using FitDataService.Domain.Models;

namespace FitDataService.Application.Services;

public class EventConsumerService : IEventConsumerService
{
    private readonly IMapper _mapper;
    private readonly IRabbitMqQueueConsumer _queueConsumer;
    private readonly IActivityFileProcessorFactory _factory;

    private const string Folder = "/shared/data";

    public EventConsumerService(
        IMapper mapper,
        IRabbitMqQueueConsumer queueConsumer,
        IActivityFileProcessorFactory factory)
    {
        _mapper = mapper;
        _queueConsumer = queueConsumer;
        _factory = factory;
    }
    
    public async Task<FitFileDataEntityDto> Consume()
    {
        ActivityFileResponseDto file = await _queueConsumer.BasicConsumeAsync<ActivityFileResponseDto>();

        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        IActivityFileProcessor? activityFileProcessor = _factory.GetProcessor(extension);
        
        if (activityFileProcessor is null)
            throw new Exception($"No activity file processor found for {extension}");
        
        FitFileData fileData = await activityFileProcessor.ReadActivityFile($"{Folder}/{file.FileName}");
        
        return _mapper.Map<FitFileDataEntityDto>(fileData);
    }
}