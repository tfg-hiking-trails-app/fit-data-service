namespace FitDataService.API.DTOs.RecordData;

public record CoordinateDto
{
    public DateTime Timestamp { get; set; }
    public int? PositionLat { get; set; }
    public int? PositionLong { get; set; }
}