using Common.Domain.Interfaces.Messaging;
using Common.Infrastructure.Messaging;

namespace FitDataService.Infrastructure.Messaging.Producer;

public class RabbitMqQueueProducer : AbstractRabbitMqQueueProducer
{
    public override string QueueName { get; }
    public override string ExchangeName { get; }

    public RabbitMqQueueProducer(IRabbitMqQueueProvider channelProvider) : base(channelProvider)
    {
        QueueName = GetUsingEnvironmentVariable("RABBITMQ_QUEUE_FITDATA_TO_HIKING");
        ExchangeName = GetUsingEnvironmentVariable("RABBITMQ_EXCHANGE_FIT_DATA_SERVICE");
    }
    
}