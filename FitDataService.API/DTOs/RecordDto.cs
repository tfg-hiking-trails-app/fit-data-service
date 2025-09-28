namespace FitDataService.API.DTOs;

public record RecordDto
{
    public DateTime Timestamp { get; set; }
    public double? PositionLat { get; set; }
    public double? PositionLong { get; set; }
    public double? Altitude { get; set; }
    public int? HeartRate { get; set; }
    public int? Cadence { get; set; }
    public double? Distance { get; set; }
    public double? Speed { get; set; }
    public int? Power { get; set; }
    public double? Temperature { get; set; }
    public int? GpsAccuracy { get; set; }
    public int? Calories { get; set; }
    public double? VerticalSpeed { get; set; }
}