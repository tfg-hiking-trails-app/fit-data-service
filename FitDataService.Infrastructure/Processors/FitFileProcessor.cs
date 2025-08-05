using Dynastream.Fit;
using FitDataService.Domain.Interfaces;
using FitDataService.Domain.Interfaces.Processors;
using FitDataService.Domain.Models;
using FitDataService.Infrastructure.Garmin;
using Activity = FitDataService.Domain.Models.Activity;

namespace FitDataService.Infrastructure.Processors;

public class FitFileProcessor : IActivityFileProcessor
{
    private readonly IFitFileDataRepository _fitFileDataRepository;
    
    public string ExtensionFile => ".fit";

    public FitFileProcessor(IFitFileDataRepository fitFileDataRepository)
    {
        _fitFileDataRepository = fitFileDataRepository;
    }
    
    public async Task ReadActivityFile(string filePath)
    {
        using (FileStream fitSource = new FileStream(filePath, FileMode.Open))
        {
            Decode decoder = new Decode();
            FitListener fitListener = new FitListener();
            
            decoder.MesgEvent += fitListener.OnMesg;

            if (!decoder.IsFIT(fitSource))
                throw new FileNotFoundException("File is not FIT file", filePath);
            
            if (!decoder.CheckIntegrity(fitSource))
                throw new FileLoadException("FIT File is not pass verification integrity", filePath);

            decoder.Read(fitSource);

            FitFileData fitFileData = CreateFitFileData(filePath, fitListener.FitMessages);

            await _fitFileDataRepository.CreateAsync(fitFileData);
        }
    }

    private FitFileData CreateFitFileData(string filePath, FitMessages fitMessages)
    {
        DecodeFitFile decodeFitFile = new DecodeFitFile();
        
        FileId fileId = decodeFitFile.CreateFileId(fitMessages.FileIdMesgs);
        Activity activity = decodeFitFile.CreateActivity(fitMessages.ActivityMesgs);
        List<Session> sessions = decodeFitFile.CreateSessions(fitMessages.SessionMesgs);
        List<Lap> laps = decodeFitFile.CreateLaps(fitMessages.LapMesgs);
        List<Record> records = decodeFitFile.CreateRecords(fitMessages.RecordMesgs);

        string hikingTrailCode = GetHikingTrailCode(filePath);

        return new FitFileData
        {
            HikingTrailCode = hikingTrailCode,
            FileId = fileId,
            Activity = activity,
            Session = sessions,
            Lap = laps,
            Records = records,
        };
    }

    private string GetHikingTrailCode(string filePath)
    { 
        return Path
            .GetFileName(filePath)
            .Replace(ExtensionFile, string.Empty);
    }
    
}