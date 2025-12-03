using Microsoft.Extensions.Options;
using MongoDB.Driver;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Data;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }
    public IMongoCollection<ShortUrl> ShortUrls => _database.GetCollection<ShortUrl>("ShortUrls");
    public IMongoCollection<Counter> Counters => _database.GetCollection<Counter>("Counters");
}
