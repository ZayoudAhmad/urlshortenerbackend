using Microsoft.EntityFrameworkCore;
using urlshortenerbackend.Data;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public class FolderRepository : IFolderRepository
{
    private readonly AppDbContext _context;

    public FolderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Folder> AddFolderAsync(Folder folder)
    {
        _context.Folders.Add(folder);
        await _context.SaveChangesAsync();
        return folder;
    }

    public async Task DeleteFolderAsync(long id)
    {
        Folder? folder = await _context.Folders.FindAsync(id);
        
        if (folder != null)
        {
            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Folder>> GetAllFoldersAsync()
    {
        return await _context.Folders
            .Include(f => f.Links)
            .ToListAsync();
    }

    public async Task<Folder?> GetFolderByIdAsync(long id)
    {
        return await _context.Folders
            .Include(f => f.Links)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Folder> UpdateFolderAsync(Folder folder)
    {
        _context.Entry(folder).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return folder;
    }
}