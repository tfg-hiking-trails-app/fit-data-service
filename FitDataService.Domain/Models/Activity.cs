using MongoDB.Bson.Serialization.Attributes;

namespace FitDataService.Domain.Models;

public class Activity
{
    [BsonElement("timestamp")]
    public DateTime? Timestamp { get; set; }

    [BsonElement("totalTimerTime")]
    public double? TotalTimerTime { get; set; }

    [BsonElement("numSessions")]
    public int? NumSessions { get; set; }

    [BsonElement("type")]
    public int? Type { get; set; }

    [BsonElement("event")]
    public int? Event { get; set; }

    [BsonElement("eventType")]
    public int? EventType { get; set; }

    [BsonElement("localTimestamp")]
    public DateTime? LocalTimestamp { get; set; }

    [BsonElement("eventGroup")]
    public int? EventGroup { get; set; }
}