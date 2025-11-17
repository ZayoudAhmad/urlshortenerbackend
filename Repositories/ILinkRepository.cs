using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public interface ILinkRepository
{
    Task<IEnumerable<Link>> GetAllLinksAsync();
    Task<Link?> GetLinkByIdAsync(long id);
    Task<Link?> GetLinkByShortUrlAsync(string ShortUrl);
    Task<Link> AddLinkAsync(Link link);
    Task<Link> UpdateLinkAsync(Link link);
    Task DeleteLinkAsync(long id);
}
