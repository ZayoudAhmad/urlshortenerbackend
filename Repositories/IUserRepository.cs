using System;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateAsync(User entity);
    Task<User> UpdateAsync(User entity);
    Task RemoveAsync(Guid id);
}
