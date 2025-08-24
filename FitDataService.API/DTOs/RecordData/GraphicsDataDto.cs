namespace FitDataService.API.DTOs.RecordData;

public record GraphicsDataDto
{
    public DateTime Timestamp { get; set; }
    public double? Altitude { get; set; }
    public int? HeartRate { get; set; }
    public int? Cadence { get; set; }
    public double? Distance { get; set; }
    public double? Speed { get; set; }
}