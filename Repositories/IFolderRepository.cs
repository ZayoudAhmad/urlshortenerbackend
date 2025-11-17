using System;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Repositories;

public interface IFolderRepository
{
    Task<IEnumerable<Folder>> GetAllFoldersAsync();
    Task<Folder?> GetFolderByIdAsync(long id);
    Task<Folder> AddFolderAsync(Folder folder);
    Task<Folder> UpdateFolderAsync(Folder folder);
    Task DeleteFolderAsync(long id);
}
