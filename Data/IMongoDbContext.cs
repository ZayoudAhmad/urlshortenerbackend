using MongoDB.Driver;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Data;

public interface IMongoDbContext
{
    IMongoCollection<ShortUrl> ShortUrls { get; }
    IMongoCollection<Counter> Counters { get; }
}
