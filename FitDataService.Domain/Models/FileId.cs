using MongoDB.Bson.Serialization.Attributes;

namespace FitDataService.Domain.Models;

public class FileId
{
    [BsonElement("type")]
    public int Type { get; set; }

    [BsonElement("manufacturer")]
    [BsonIgnoreIfNull]
    public int? Manufacturer { get; set; }

    [BsonElement("product")]
    [BsonIgnoreIfNull]
    public int? Product { get; set; }

    [BsonElement("faveroProduct")]
    [BsonIgnoreIfNull]
    public int? FaveroProduct { get; set; }

    [BsonElement("garminProduct")]
    [BsonIgnoreIfNull]
    public int? GarminProduct { get; set; }

    [BsonElement("serialNumber")]
    [BsonIgnoreIfNull]
    public long? SerialNumber { get; set; }

    [BsonElement("timeCreated")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonIgnoreIfNull]
    public DateTime? TimeCreated { get; set; }

    [BsonElement("number")]
    [BsonIgnoreIfNull]
    public int? Number { get; set; }

    [BsonElement("productName")]
    [BsonIgnoreIfNull]
    public string? ProductName { get; set; }
}