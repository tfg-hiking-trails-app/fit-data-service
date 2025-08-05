namespace FitDataService.Domain.Interfaces.Processors;

public interface IActivityFileProcessor
{
    string ExtensionFile { get; }
    Task ReadActivityFile(string filePath);
}