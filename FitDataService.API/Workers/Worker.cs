using FitDataService.Application.DTOs.Messaging;
using FitDataService.Application.Interfaces;

namespace FitDataService.API.Workers
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger)
        {
            _serviceScopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using IServiceScope scope = _serviceScopeFactory.CreateScope();

                    IEventConsumerService consumer = scope.ServiceProvider
                        .GetRequiredService<IEventConsumerService>();
                    IEventProducerService producer = scope.ServiceProvider
                        .GetRequiredService<IEventProducerService>();

                    FitFileResultDto result = await consumer.Consume();

                    await producer.Send(result);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled error while processing an activity file");
                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }
            }
        }
    }
}
