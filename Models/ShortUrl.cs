using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace urlshortenerbackend.Models;

// the purpose of this table is to be stored in a nosql database (mongodb) for fast reads to have a rapid redirection
public class ShortUrl
{
    [BsonId]
    public required long Id { get; set; }

    [BsonElement("alias")]
    public required string Alias { get; set; }

    [BsonElement("destinationUrl")]
    public required string DestinationUrl { get; set; }
}
