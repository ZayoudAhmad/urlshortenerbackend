using Microsoft.Extensions.Options;
using MongoDB.Driver;
using urlshortenerbackend.Data;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public class ShortUrlRepository : IShortUrlRepository
{
    private readonly IMongoCollection<ShortUrl> _shortUrlCollection;

    public ShortUrlRepository(IMongoDbContext dbContext)
    {
        _shortUrlCollection = dbContext.ShortUrls;
    }

    public async Task<ShortUrl> CreateAsync(ShortUrl newShortUrl)
    {
        await _shortUrlCollection.InsertOneAsync(newShortUrl);
        return newShortUrl;
    }

    public async Task<IEnumerable<ShortUrl>> GetAllShortUrlsAsync()
    {
        return await _shortUrlCollection.Find(_ => true).ToListAsync();
    }

    public async Task<ShortUrl?> GetByAliasAsync(string alias)
    {
        return await _shortUrlCollection.Find(shortUrl => shortUrl.Alias == alias).FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(string alias)
    {
        await _shortUrlCollection.DeleteOneAsync(x => x.Alias == alias);
    }

    public async Task<ShortUrl> UpdateAsync(string alias, ShortUrl updatedShortUrl)
    {
        await _shortUrlCollection.ReplaceOneAsync(x => x.Alias == alias, updatedShortUrl);
        return updatedShortUrl;
    }
}
