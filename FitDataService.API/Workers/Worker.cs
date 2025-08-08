using FitDataService.Application.DTOs.Messaging;
using FitDataService.Application.Interfaces;

namespace FitDataService.API.Workers
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(IServiceScopeFactory scopeFactory)
        {
            _serviceScopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    IEventConsumerService consumer = scope.ServiceProvider
                        .GetRequiredService<IEventConsumerService>();
                    IEventProducerService producer = scope.ServiceProvider
                        .GetRequiredService<IEventProducerService>();
                    
                    FitFileDataEntityDto fileData = await consumer.Consume();

                    await producer.Send(fileData);
                }
            }
        }
    }
}
