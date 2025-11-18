using System;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public interface IClickLogRepository
{
    Task<IEnumerable<ClickLog>> GetAllClickLogAsync();
    Task<ClickLog?> GetClickLogByIdAsync(long id);
    Task<ClickLog> AddClickLogAsync(ClickLog clickLog);
    Task<ClickLog> UpdateClickLogAsync(ClickLog clickLog);
    Task DeleteClickLogAsync(long id);
}
