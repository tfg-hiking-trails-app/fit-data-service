namespace FitDataService.Application.DTOs.Messaging;

public record FitFileResultDto
{
    public bool IsValid { get; init; }
    public string? RejectionReason { get; init; }
    public FitFileDataEntityDto? Data { get; init; }

    public static FitFileResultDto Valid(FitFileDataEntityDto data) =>
        new() { IsValid = true, Data = data };

    public static FitFileResultDto Rejected(string reason) =>
        new() { IsValid = false, RejectionReason = reason };
}
