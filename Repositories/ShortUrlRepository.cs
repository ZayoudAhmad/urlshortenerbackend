using Microsoft.Extensions.Options;
using MongoDB.Driver;
using urlshortenerbackend.Data;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public class ShortUrlRepository
{
    private readonly IMongoCollection<ShortUrl> _shortUrlCollection;

    public ShortUrlRepository(IOptions<MongoDbSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
    }

}
