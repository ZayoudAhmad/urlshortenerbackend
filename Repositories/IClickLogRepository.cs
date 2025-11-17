using System;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public interface IClickLogRepository
{
    Task<IEnumerable<ClickLog>> GetAllClickLogAsync();
    Task<ClickLog?> GetClickLogByIdAsync();
    Task<ClickLog> AddClickLogAsync(ClickLog clickLog);
    Task<ClickLog> UpdateClickLogAsync(ClickLog clickLog);
    Task DeleteClickLogAsync(long id);
}
