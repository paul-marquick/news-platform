using Microsoft.EntityFrameworkCore;
using NewsPlatform.ConsoleApp1.Entities;

namespace NewsPlatform.ConsoleApp1;

public class NewsPlatformContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=paul-wins-11; Database=NewsPlatform; Trusted_Connection=True; Encrypt=false;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Article>()
            .ToTable("Article");

        modelBuilder.Entity<Category>()
            .ToTable("Category");
    }
}