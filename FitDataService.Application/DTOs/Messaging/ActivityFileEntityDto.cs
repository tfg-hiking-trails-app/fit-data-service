namespace FitDataService.Application.DTOs.Messaging;

public class ActivityFileResponseDto
{
    public string? ContentType { get; set; }
    public required string FileName { get; set; }
}