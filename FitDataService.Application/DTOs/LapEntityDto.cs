namespace FitDataService.Application.DTOs;

public record LapEntityDto
{
    public DateTime Timestamp { get; set; }
    public DateTime? StartTime { get; set; }
    public double? TotalElapsedTime { get; set; }
    public double? TotalTimerTime { get; set; }
    public double? TotalDistance { get; set; }
    public int? TotalCalories { get; set; }
    public double? AvgSpeed { get; set; }
    public double? MaxSpeed { get; set; }
    public int? AvgHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public int? AvgCadence { get; set; }
    public int? MaxCadence { get; set; }
    public int? AvgPower { get; set; }
    public int? MaxPower { get; set; }
    public int? TotalAscent { get; set; }
    public int? TotalDescent { get; set; }
    public int? Intensity { get; set; }
    public int? LapTrigger { get; set; }
    public int? Sport { get; set; }
    public double? AvgTemperature { get; set; }
    public double? MaxTemperature { get; set; }
    public double? AvgPace { get; set; } // s/km
    public double? MaxPace { get; set; } // s/km
}