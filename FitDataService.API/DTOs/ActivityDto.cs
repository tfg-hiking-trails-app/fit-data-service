namespace FitDataService.API.DTOs;

public record ActivityDto
{
    public DateTime? Timestamp { get; set; }
    public double? TotalTimerTime { get; set; }
    public int? NumSessions { get; set; }
    public int? Type { get; set; }
    public int? Event { get; set; }
    public int? EventType { get; set; }
    public DateTime? LocalTimestamp { get; set; }
    public int? EventGroup { get; set; }
}