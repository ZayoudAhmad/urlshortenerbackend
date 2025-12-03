using MongoDB.Driver;
using urlshortenerbackend.Data;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Services;

public class SequentialIdGenerator : ISequentialIdGenerator
{
    private readonly IMongoCollection<Counter> _counterCollection;
    private readonly IMongoCollection<ShortUrl> _shortUrlCollection;
    private readonly string _targetCollectionName = "ShortUrls";

    private bool _initialized = false;

    public SequentialIdGenerator(IMongoDbContext context, ILogger<SequentialIdGenerator> logger)
    {
        _counterCollection = context.Counters;
        _shortUrlCollection = context.ShortUrls;
    }

    private async Task EnsureInitializedAsync()
    {
        if (_initialized)
            return;

        var existingCounter = await _counterCollection
            .Find(c => c.CollectionName == _targetCollectionName)
            .FirstOrDefaultAsync();


        if (existingCounter == null)
        {
            long maxId = 0;

            var lastShortUrl = await _shortUrlCollection
                .Find(Builders<ShortUrl>.Filter.Empty)
                .SortByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (lastShortUrl != null)
            {
                maxId = lastShortUrl.Id;
            }

            var newCounter = new Counter
            {
                CollectionName = _targetCollectionName,
                SequenceValue = maxId
            };

            try
            {
                await _counterCollection.InsertOneAsync(newCounter);
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
            }
        }

        _initialized = true;
    }

    public async Task<long> GenerateIdAsync()
    {
        await EnsureInitializedAsync();

        var filter = Builders<Counter>.Filter.Eq(c => c.CollectionName, _targetCollectionName);
        var update = Builders<Counter>.Update.Inc(c => c.SequenceValue, 1);

        var options = new FindOneAndUpdateOptions<Counter>
        {
            ReturnDocument = ReturnDocument.After,
            IsUpsert = false
        };

        var result = await _counterCollection.FindOneAndUpdateAsync(filter, update, options);

        if (result == null)
        {
            throw new InvalidOperationException($"Counter for '{_targetCollectionName}' not found.");
        }

        return result.SequenceValue;
    }
}