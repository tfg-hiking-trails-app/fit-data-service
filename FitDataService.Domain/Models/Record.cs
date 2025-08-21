using MongoDB.Bson.Serialization.Attributes;

namespace FitDataService.Domain.Models;

public class Record
{
    [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; }

    [BsonElement("position_lat")]
    public double? PositionLat { get; set; }

    [BsonElement("position_long")]
    public double? PositionLong { get; set; }

    [BsonElement("altitude")]
    public double? Altitude { get; set; }

    [BsonElement("heart_rate")]
    public int? HeartRate { get; set; }

    [BsonElement("cadence")]
    public int? Cadence { get; set; }

    [BsonElement("distance")]
    public double? Distance { get; set; }

    [BsonElement("speed")]
    public double? Speed { get; set; }

    [BsonElement("power")]
    public int? Power { get; set; }

    [BsonElement("temperature")]
    public double? Temperature { get; set; }

    [BsonElement("gps_accuracy")]
    public int? GpsAccuracy { get; set; }
    
    [BsonElement("calories")]
    public int? Calories { get; set; }

    [BsonElement("vertical_speed")]
    public double? VerticalSpeed { get; set; }
}