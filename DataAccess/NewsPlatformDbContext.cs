using Microsoft.EntityFrameworkCore;
using NewsPlatform.DataAccess.Entities;

namespace NewsPlatform.DataAccess;

public class NewsPlatformDbContext : DbContext
{
    public NewsPlatformDbContext(DbContextOptions<NewsPlatformDbContext> options) : base(options) { }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Article>()
            .ToTable("Article");

        modelBuilder.Entity<Category>()
            .ToTable("Category");
    }
}