using MongoDB.Bson.Serialization.Attributes;

namespace FitDataService.Domain.Models;

public class FileId
{
    [BsonElement("type")]
    public int? Type { get; set; }

    [BsonElement("manufacturer")]
    public int? Manufacturer { get; set; }

    [BsonElement("product")]
    public int? Product { get; set; }

    [BsonElement("faveroProduct")]
    public int? FaveroProduct { get; set; }

    [BsonElement("garminProduct")]
    public int? GarminProduct { get; set; }

    [BsonElement("serialNumber")]
    public long? SerialNumber { get; set; }

    [BsonElement("timeCreated")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? TimeCreated { get; set; }

    [BsonElement("number")]
    public int? Number { get; set; }

    [BsonElement("productName")]
    public string? ProductName { get; set; }
}