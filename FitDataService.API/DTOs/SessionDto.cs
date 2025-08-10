namespace FitDataService.API.DTOs;

public record SessionDto
{
    public int? MessageIndex { get; set; }
    public DateTime Timestamp { get; set; }
    public int? Event { get; set; }
    public int? EventType { get; set; }
    public DateTime StartTime { get; set; }
    public int? StartPositionLat { get; set; }
    public int? StartPositionLong { get; set; }
    public int? Sport { get; set; }
    public int? SubSport { get; set; }
    public double TotalElapsedTime { get; set; }
    public double TotalTimerTime { get; set; }
    public double? TotalDistance { get; set; }
    public long? TotalCycles { get; set; }
    public long? TotalStrides { get; set; }
    public long? TotalStrokes { get; set; }
    public int? TotalCalories { get; set; }
    public int? TotalFatCalories { get; set; }
    public double? AvgSpeed { get; set; }
    public double? MaxSpeed { get; set; }
    public int? AvgHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public int? MinHeartRate { get; set; }
    public int? AvgCadence { get; set; }
    public int? AvgRunningCadence { get; set; }
    public int? MaxCadence { get; set; }
    public int? MaxRunningCadence { get; set; }
    public int? AvgPower { get; set; }
    public int? MaxPower { get; set; }
    public int? TotalAscent { get; set; }
    public int? TotalDescent { get; set; }
    public float? AvgAltitude { get; set; }
    public float? MaxAltitude { get; set; }
    public float? MinAltitude { get; set; }
    public double? TotalTrainingEffect { get; set; }
    public int? FirstLapIndex { get; set; }
    public int? NumLaps { get; set; }
    public int? EventGroup { get; set; }
    public int? Trigger { get; set; }
    public int? NecLat { get; set; }
    public int? NecLong { get; set; }
    public int? SwcLat { get; set; }
    public int? SwcLong { get; set; }
    public int? NumLengths { get; set; }
    public int? NormalizedPower { get; set; }
    public double? TrainingStressScore { get; set; }
    public double? IntensityFactor { get; set; }
    public int? LeftRightBalance { get; set; }
    public int? EndPositionLat { get; set; }
    public int? EndPositionLong { get; set; }
}