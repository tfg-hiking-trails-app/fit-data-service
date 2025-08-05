using MongoDB.Bson.Serialization.Attributes;

namespace FitDataService.Domain.Models;

public class Activity
{
    [BsonElement("timestamp")]
    [BsonIgnoreIfNull]
    public DateTime? Timestamp { get; set; }

    [BsonElement("totalTimerTime")]
    [BsonIgnoreIfNull]
    public double? TotalTimerTime { get; set; }

    [BsonElement("numSessions")]
    [BsonIgnoreIfNull]
    public int? NumSessions { get; set; }

    [BsonElement("type")]
    [BsonIgnoreIfNull]
    public int? Type { get; set; }

    [BsonElement("event")]
    [BsonIgnoreIfNull]
    public int? Event { get; set; }

    [BsonElement("eventType")]
    [BsonIgnoreIfNull]
    public int? EventType { get; set; }

    [BsonElement("localTimestamp")]
    [BsonIgnoreIfNull]
    public DateTime? LocalTimestamp { get; set; }

    [BsonElement("eventGroup")]
    [BsonIgnoreIfNull]
    public int? EventGroup { get; set; }
}