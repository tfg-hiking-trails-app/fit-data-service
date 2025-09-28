namespace FitDataService.API.DTOs;

public record FileIdDto
{
    public int? Type { get; set; }
    public int? Manufacturer { get; set; }
    public int? Product { get; set; }
    public int? FaveroProduct { get; set; }
    public int? GarminProduct { get; set; }
    public long? SerialNumber { get; set; }
    public DateTime? TimeCreated { get; set; }
    public int? Number { get; set; }
    public string? ProductName { get; set; }
}