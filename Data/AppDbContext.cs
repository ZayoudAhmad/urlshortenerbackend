using Microsoft.EntityFrameworkCore;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Link> Links => Set<Link>();
    public DbSet<Folder> Folders => Set<Folder>();
    public DbSet<ClickLog> ClickLog => Set<ClickLog>(); 
}
