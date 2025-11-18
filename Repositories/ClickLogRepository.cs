using Microsoft.EntityFrameworkCore;
using urlshortenerbackend.Data;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public class ClickLogRepository : IClickLogRepository
{
    private readonly AppDbContext _context;

    public ClickLogRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ClickLog> AddClickLogAsync(ClickLog clickLog)
    {
        _context.ClickLogs.Add(clickLog);
        await _context.SaveChangesAsync();
        return clickLog;
    }

    public async Task DeleteClickLogAsync(long id)
    {
        ClickLog? clickLog = await _context.ClickLogs.FindAsync(id);
        if (clickLog != null)
        {
            _context.ClickLogs.Remove(clickLog);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ClickLog>> GetAllClickLogAsync()
    {
        return await _context.ClickLogs.ToListAsync();
    }

    public async Task<ClickLog?> GetClickLogByIdAsync(long id)
    {
        return await _context.ClickLogs.FirstOrDefaultAsync(cl => cl.Id == id);
    }

    public async Task<ClickLog> UpdateClickLogAsync(ClickLog clickLog)
    {
        _context.Entry(clickLog).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return clickLog;
    }
}
