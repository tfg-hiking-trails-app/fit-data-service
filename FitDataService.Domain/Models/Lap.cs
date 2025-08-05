using MongoDB.Bson.Serialization.Attributes;

namespace FitDataService.Domain.Models;

public class Lap
{
     [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; }

    [BsonElement("start_time")]
    [BsonIgnoreIfNull]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? StartTime { get; set; }

    [BsonElement("total_elapsed_time")]
    [BsonIgnoreIfNull]
    public double? TotalElapsedTime { get; set; }

    [BsonElement("total_timer_time")]
    [BsonIgnoreIfNull]
    public double? TotalTimerTime { get; set; }

    [BsonElement("total_distance")]
    [BsonIgnoreIfNull]
    public double? TotalDistance { get; set; }

    [BsonElement("total_calories")]
    [BsonIgnoreIfNull]
    public int? TotalCalories { get; set; }

    [BsonElement("avg_speed")]
    [BsonIgnoreIfNull]
    public double? AvgSpeed { get; set; }

    [BsonElement("max_speed")]
    [BsonIgnoreIfNull]
    public double? MaxSpeed { get; set; }

    [BsonElement("avg_heart_rate")]
    [BsonIgnoreIfNull]
    public int? AvgHeartRate { get; set; }

    [BsonElement("max_heart_rate")]
    [BsonIgnoreIfNull]
    public int? MaxHeartRate { get; set; }

    [BsonElement("avg_cadence")]
    [BsonIgnoreIfNull]
    public int? AvgCadence { get; set; }

    [BsonElement("max_cadence")]
    [BsonIgnoreIfNull]
    public int? MaxCadence { get; set; }

    [BsonElement("avg_power")]
    [BsonIgnoreIfNull]
    public int? AvgPower { get; set; }

    [BsonElement("max_power")]
    [BsonIgnoreIfNull]
    public int? MaxPower { get; set; }

    [BsonElement("total_ascent")]
    [BsonIgnoreIfNull]
    public int? TotalAscent { get; set; }

    [BsonElement("total_descent")]
    [BsonIgnoreIfNull]
    public int? TotalDescent { get; set; }

    [BsonElement("intensity")]
    [BsonIgnoreIfNull]
    public int? Intensity { get; set; }

    [BsonElement("lap_trigger")]
    [BsonIgnoreIfNull]
    public int? LapTrigger { get; set; }

    [BsonElement("sport")]
    [BsonIgnoreIfNull]
    public int? Sport { get; set; }

    [BsonElement("avg_temperature")]
    [BsonIgnoreIfNull]
    public double? AvgTemperature { get; set; }

    [BsonElement("max_temperature")]
    [BsonIgnoreIfNull]
    public double? MaxTemperature { get; set; }
}