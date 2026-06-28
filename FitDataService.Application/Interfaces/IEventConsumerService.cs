using FitDataService.Application.DTOs.Messaging;

namespace FitDataService.Application.Interfaces;

public interface IEventConsumerService
{
    Task<FitFileResultDto> Consume();
}