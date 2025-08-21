namespace FitDataService.Application.DTOs.RecordData;

public record CoordinateEntityDto
{
    public DateTime Timestamp { get; set; }
    public double? PositionLat { get; set; }
    public double? PositionLong { get; set; }
}