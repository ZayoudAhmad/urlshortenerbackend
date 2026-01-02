using Microsoft.EntityFrameworkCore;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<ShortUrlMeta> shortUrlMetas => Set<ShortUrlMeta>();
    public DbSet<Folder> Folders => Set<Folder>();
    public DbSet<ClickEvent> ClickEvents => Set<ClickEvent>();
}
