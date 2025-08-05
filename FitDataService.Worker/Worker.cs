using FitDataService.Application.Interfaces;

namespace FitDataService.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEventConsumerService _eventConsumerService;

        public Worker(
            ILogger<Worker> logger,
            IEventConsumerService eventConsumerService)
        {
            _logger = logger;
            _eventConsumerService = eventConsumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Pedir el CODE de Hiking trail y con ese code llamar al servicio de decode .fit
                await _eventConsumerService.Consume();
                
                
                // TODO:
                // Leer .fit y decodiciar con garmin
                // Guardar datos en mongo
                // Enviar a Hiking Trail Service la data resumida para la tablas SQL
            }
        }
    }
}
