using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using urlshortenerbackend.Models;

namespace urlshortenerbackend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Link> Links => Set<Link>();
    public DbSet<Folder> Folders => Set<Folder>();
    public DbSet<ClickLog> ClickLog => Set<ClickLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Link>().HasIndex(l => l.ShortUrl).IsUnique();

        modelBuilder.Entity<ClickLog>().HasOne(cl => cl.Link).WithMany(l => l.ClickLogs).HasForeignKey(cl => cl.LinkId).OnDelete(DeleteBehavior.Cascade);
    }
}
