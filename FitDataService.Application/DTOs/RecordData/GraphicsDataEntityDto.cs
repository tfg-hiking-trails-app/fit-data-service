namespace FitDataService.Application.DTOs.RecordData;

public record GraphicsDataEntityDto
{
    public DateTime Timestamp { get; set; }
    public double? Altitude { get; set; }
    public int? HeartRate { get; set; }
    public int? Cadence { get; set; }
    public double? Distance { get; set; }
    public double? Speed { get; set; }
}