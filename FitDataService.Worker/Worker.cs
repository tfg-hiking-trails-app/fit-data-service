using FitDataService.Application.DTOs.Messaging;
using FitDataService.Application.Interfaces;

namespace FitDataService.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IEventConsumerService _eventConsumerService;
        private readonly IEventProducerService _eventProducerService;

        public Worker(
            IEventConsumerService eventConsumerService,
            IEventProducerService eventProducerService)
        {
            _eventConsumerService = eventConsumerService;
            _eventProducerService = eventProducerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                FitFileDataEntityDto fileData = await _eventConsumerService.Consume();

                await _eventProducerService.Send(fileData);
            }
        }
    }
}
