using FitDataService.Domain.Models;

namespace FitDataService.Domain.Interfaces.Processors;

public interface IActivityFileProcessor
{
    string ExtensionFile { get; }
    Task<FitFileData> ReadActivityFile(string filePath);
}