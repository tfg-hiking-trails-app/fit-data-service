namespace FitDataService.Application.Interfaces;

public interface IEventConsumerService
{
    Task<string> Consume();
}