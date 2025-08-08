using FitDataService.Application.DTOs.Messaging;

namespace FitDataService.Application.Interfaces;

public interface IEventProducerService
{
    Task Send(FitFileDataEntityDto fileDataEntityDto);
}