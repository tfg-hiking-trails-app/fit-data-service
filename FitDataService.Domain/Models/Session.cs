using MongoDB.Bson.Serialization.Attributes;

namespace FitDataService.Domain.Models;

public class Session
{
     [BsonElement("messageIndex")]
    [BsonIgnoreIfNull]
    public int? MessageIndex { get; set; }

    [BsonElement("timestamp")]
    public long Timestamp { get; set; }

    [BsonElement("event")]
    [BsonIgnoreIfNull]
    public int? Event { get; set; }

    [BsonElement("eventType")]
    [BsonIgnoreIfNull]
    public int? EventType { get; set; }

    [BsonElement("startTime")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime StartTime { get; set; }

    [BsonElement("startPositionLat")]
    [BsonIgnoreIfNull]
    public int? StartPositionLat { get; set; }

    [BsonElement("startPositionLong")]
    [BsonIgnoreIfNull]
    public int? StartPositionLong { get; set; }

    [BsonElement("sport")]
    [BsonIgnoreIfNull]
    public int? Sport { get; set; }

    [BsonElement("subSport")]
    [BsonIgnoreIfNull]
    public int? SubSport { get; set; }

    [BsonElement("totalElapsedTime")]
    public double TotalElapsedTime { get; set; }

    [BsonElement("totalTimerTime")]
    public double TotalTimerTime { get; set; }

    [BsonElement("totalDistance")]
    [BsonIgnoreIfNull]
    public double? TotalDistance { get; set; }

    [BsonElement("totalCycles")]
    [BsonIgnoreIfNull]
    public long? TotalCycles { get; set; }

    [BsonElement("totalStrides")]
    [BsonIgnoreIfNull]
    public long? TotalStrides { get; set; }

    [BsonElement("totalStrokes")]
    [BsonIgnoreIfNull]
    public long? TotalStrokes { get; set; }

    [BsonElement("totalCalories")]
    [BsonIgnoreIfNull]
    public int? TotalCalories { get; set; }

    [BsonElement("totalFatCalories")]
    [BsonIgnoreIfNull]
    public int? TotalFatCalories { get; set; }

    [BsonElement("avgSpeed")]
    [BsonIgnoreIfNull]
    public double? AvgSpeed { get; set; }

    [BsonElement("maxSpeed")]
    [BsonIgnoreIfNull]
    public double? MaxSpeed { get; set; }

    [BsonElement("avgHeartRate")]
    [BsonIgnoreIfNull]
    public int? AvgHeartRate { get; set; }

    [BsonElement("maxHeartRate")]
    [BsonIgnoreIfNull]
    public int? MaxHeartRate { get; set; }

    [BsonElement("avgCadence")]
    [BsonIgnoreIfNull]
    public int? AvgCadence { get; set; }

    [BsonElement("avgRunningCadence")]
    [BsonIgnoreIfNull]
    public int? AvgRunningCadence { get; set; }

    [BsonElement("maxCadence")]
    [BsonIgnoreIfNull]
    public int? MaxCadence { get; set; }

    [BsonElement("maxRunningCadence")]
    [BsonIgnoreIfNull]
    public int? MaxRunningCadence { get; set; }

    [BsonElement("avgPower")]
    [BsonIgnoreIfNull]
    public int? AvgPower { get; set; }

    [BsonElement("maxPower")]
    [BsonIgnoreIfNull]
    public int? MaxPower { get; set; }

    [BsonElement("totalAscent")]
    [BsonIgnoreIfNull]
    public int? TotalAscent { get; set; }

    [BsonElement("totalDescent")]
    [BsonIgnoreIfNull]
    public int? TotalDescent { get; set; }

    [BsonElement("totalTrainingEffect")]
    [BsonIgnoreIfNull]
    public double? TotalTrainingEffect { get; set; }

    [BsonElement("firstLapIndex")]
    [BsonIgnoreIfNull]
    public int? FirstLapIndex { get; set; }

    [BsonElement("numLaps")]
    [BsonIgnoreIfNull]
    public int? NumLaps { get; set; }

    [BsonElement("eventGroup")]
    [BsonIgnoreIfNull]
    public int? EventGroup { get; set; }

    [BsonElement("trigger")]
    [BsonIgnoreIfNull]
    public int? Trigger { get; set; }

    [BsonElement("necLat")]
    [BsonIgnoreIfNull]
    public int? NecLat { get; set; }

    [BsonElement("necLong")]
    [BsonIgnoreIfNull]
    public int? NecLong { get; set; }

    [BsonElement("swcLat")]
    [BsonIgnoreIfNull]
    public int? SwcLat { get; set; }

    [BsonElement("swcLong")]
    [BsonIgnoreIfNull]
    public int? SwcLong { get; set; }

    [BsonElement("numLengths")]
    [BsonIgnoreIfNull]
    public int? NumLengths { get; set; }

    [BsonElement("normalizedPower")]
    [BsonIgnoreIfNull]
    public int? NormalizedPower { get; set; }

    [BsonElement("trainingStressScore")]
    [BsonIgnoreIfNull]
    public double? TrainingStressScore { get; set; }

    [BsonElement("intensityFactor")]
    [BsonIgnoreIfNull]
    public double? IntensityFactor { get; set; }

    [BsonElement("leftRightBalance")]
    [BsonIgnoreIfNull]
    public int? LeftRightBalance { get; set; }

    [BsonElement("endPositionLat")]
    [BsonIgnoreIfNull]
    public int? EndPositionLat { get; set; }

    [BsonElement("endPositionLong")]
    [BsonIgnoreIfNull]
    public int? EndPositionLong { get; set; }
}