using MongoDB.Bson.Serialization.Attributes;

namespace urlshortenerbackend.Models;

// the purpose of this table is to be stored in a nosql database (mongodb) for fast reads to have a rapid redirection
public class ShortUrl
{
    [BsonId]
    public string Alias { get; set; } = default!;

    [BsonElement("destinationUrl")]
    public string DestinationUrl { get; set; } = default!;
}
