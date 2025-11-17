using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public class ClickLogRepository : IClickLogRepository
{
    public Task<ClickLog> AddClickLogAsync(ClickLog clickLog)
    {
        throw new NotImplementedException();
    }

    public Task DeleteClickLogAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClickLog>> GetAllClickLogAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ClickLog?> GetClickLogByIdAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ClickLog> UpdateClickLogAsync(ClickLog clickLog)
    {
        throw new NotImplementedException();
    }
}
