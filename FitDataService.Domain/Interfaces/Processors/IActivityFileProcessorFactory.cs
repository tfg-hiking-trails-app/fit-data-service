namespace FitDataService.Domain.Interfaces.Processors;

public interface IActivityFileProcessorFactory
{
    IActivityFileProcessor? GetProcessor(string extensionFile);
}