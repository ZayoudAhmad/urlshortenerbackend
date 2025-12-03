using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public interface IShortUrlRepository
{
    Task<IEnumerable<ShortUrl>> GetAllShortUrlsAsync();
    Task<ShortUrl?> GetByAliasAsync(string alias);
    Task<ShortUrl> CreateAsync(ShortUrl newShortUrl);
    Task<ShortUrl> UpdateAsync(string alias, ShortUrl updatedShortUrl);
    Task RemoveAsync(string alias);
}
