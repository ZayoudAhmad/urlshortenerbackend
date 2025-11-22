using System;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public interface IShortUrlRepository
{
    Task<ShortUrl?> GetByAliasAsync(string alias);
    Task CreateAsync(ShortUrl newShortUrl);
    Task UpdateAsync(string alias, ShortUrl updatedShortUrl);
    Task RemoveAsync(string alias);
}
