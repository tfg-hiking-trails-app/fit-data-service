using FitDataService.Domain.Interfaces.Processors;

namespace FitDataService.Infrastructure.Processors;

public class ActivityFileProcessorFactory : IActivityFileProcessorFactory
{
    private readonly Dictionary<string, IActivityFileProcessor> _processors;
    
    public ActivityFileProcessorFactory(IEnumerable<IActivityFileProcessor> processors)
    {
        _processors = processors.ToDictionary(x => 
                x.ExtensionFile, 
            x => x);
    }

    public IActivityFileProcessor? GetProcessor(string extensionFile)
    {
        return _processors.GetValueOrDefault(extensionFile);
    }
    
}