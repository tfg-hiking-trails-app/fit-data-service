namespace FitDataService.Application.DTOs.Messaging;

public record FitFileDataEntityDto
{
    public required Guid HikingTrailCode { get; set; }
    public required string Name { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required double UbicationLatitude { get; set; }
    public required double UbicationLongitude { get; set; }
    public required bool GeneratedByFitFile { get; set; }
    public required int Distance { get; set; }
    public required double Duration { get; set; }
    public required long Steps { get; set; }
    public required int Calories { get; set; }
    public required double AveragePace { get; set; }
    public required double MaxPace { get; set; }
    public required double ElevationGain { get; set; }
    public required double ElevationLoss { get; set; }
    public required double AverageSpeed { get; set; }
    public required double MaxSpeed { get; set; }
    public required int AverageHeartRate { get; set; }
    public required int MaxHeartRate { get; set; }
    public required int MinHeartRate { get; set; }
    public required double AverageCadence { get; set; }
    public required double MaxCadence { get; set; }
    public required double MaxAltitude { get; set; }
    public required double MinAltitude { get; set; }
    public required double TotalTrainingEffect { get; set; }
    public required double TrainingStressScore { get; set; }
}