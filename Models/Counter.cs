using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace urlshortenerbackend.Models;

public class Counter
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required string CollectionName { get; set; }

    [BsonElement("sequenceValue")]
    [BsonRepresentation(BsonType.Int64)]
    public required long SequenceValue { get; set; }
}