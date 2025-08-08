using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitDataService.Domain.Models;

public class FitFileData : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("hiking_trail_code")]
    public string? HikingTrailCode { get; set; }
    
    [BsonElement("file_id")]
    public FileId FileId { get; set; } = null!;

    [BsonElement("activity")]
    public Activity Activity { get; set; } = null!;

    [BsonElement("session")]
    public IList<Session> Session { get; set; } = new List<Session>();

    [BsonElement("lap")]
    public IList<Lap>? Lap { get; set; } = new List<Lap>();

    [BsonElement("records")]
    public IList<Record> Records { get; set; } = new List<Record>();
}