namespace FitDataService.Application.DTOs.RecordData;

public record CoordinateEntityDto
{
    public DateTime Timestamp { get; set; }
    public int? PositionLat { get; set; }
    public int? PositionLong { get; set; }
}