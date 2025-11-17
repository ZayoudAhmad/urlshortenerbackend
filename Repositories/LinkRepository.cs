using Microsoft.EntityFrameworkCore;
using urlshortenerbackend.Data;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public class LinkRepository : ILinkRepository
{
    private readonly AppDbContext _context;

    public LinkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Link> AddLinkAsync(Link link)
    {
        _context.Links.Add(link);
        await _context.SaveChangesAsync();
        return link;
    }

    public async Task DeleteLinkAsync(long id)
    {
        Link? link = await _context.Links.FindAsync(id);
        if (link != null)
        {
            _context.Links.Remove(link!);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Link>> GetAllLinksAsync()
    {
        return await _context.Links.Include(l => l.Folder).ToListAsync();
    }

    public async Task<Link?> GetLinkByIdAsync(long id)
    {
        return await _context.Links.Include(l => l.FolderId).FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Link?> GetLinkByShortUrlAsync(string ShortUrl)
    {
        return await _context.Links.Include(l => l.FolderId).FirstOrDefaultAsync(l => l.ShortUrl == ShortUrl);
    }

    public async Task<Link> UpdateLinkAsync(Link link)
    {
        _context.Entry(link).State = EntityState.Modified;
        link.UpdatedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();
        return link;
    }
}
