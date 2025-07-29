using MongoDB.Bson.Serialization.Attributes;

namespace FitDataService.Domain.Models;

public class Record
{
    [BsonElement("timestamp")]
    public long Timestamp { get; set; }

    [BsonElement("position_lat")]
    public int PositionLat { get; set; }

    [BsonElement("position_long")]
    public int PositionLong { get; set; }

    [BsonElement("altitude")]
    public double Altitude { get; set; }

    [BsonElement("heart_rate")]
    [BsonIgnoreIfNull]
    public int? HeartRate { get; set; }

    [BsonElement("cadence")]
    [BsonIgnoreIfNull]
    public int? Cadence { get; set; }

    [BsonElement("distance")]
    [BsonIgnoreIfNull]
    public double? Distance { get; set; }

    [BsonElement("speed")]
    [BsonIgnoreIfNull]
    public double? Speed { get; set; }

    [BsonElement("power")]
    [BsonIgnoreIfNull]
    public int? Power { get; set; }

    [BsonElement("temperature")]
    [BsonIgnoreIfNull]
    public double? Temperature { get; set; }

    [BsonElement("gps_accuracy")]
    [BsonIgnoreIfNull]
    public int? GpsAccuracy { get; set; }

    [BsonElement("calories")]
    [BsonIgnoreIfNull]
    public int? Calories { get; set; }

    [BsonElement("vertical_speed")]
    [BsonIgnoreIfNull]
    public double? VerticalSpeed { get; set; }
}