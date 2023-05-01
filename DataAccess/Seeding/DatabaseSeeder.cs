using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsPlatform.DataAccess.Entities;

namespace NewsPlatform.DataAccess.Seeding;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly NewsPlatformDbContext dbContext;
    private readonly ILogger<DatabaseSeeder> logger;

    public DatabaseSeeder(NewsPlatformDbContext dbContext, ILogger<DatabaseSeeder> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task SeedDatabase()
    {
        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Article]");
        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Category]");

        var cat1 = new Category { Name = "Sport" };
        dbContext.Categories.Add(cat1);
        await dbContext.SaveChangesAsync();

        var art1 = new Article { CategoryId = cat1.Id, Title = "Exeter Chiefs Win Again", Content = "The Chiefs easily beat Saracens 67 - 0." };
        dbContext.Articles.Add(art1);
        await dbContext.SaveChangesAsync();

        var art2 = new Article { CategoryId = cat1.Id, Title = "Honda 1,2,3 at Silverstone", Content = "Marc Marquez led home a Honda 1,2,3 at the British MotoGP." };
        dbContext.Articles.Add(art2);
        await dbContext.SaveChangesAsync();

        var cat2 = new Category { Name = "Travel" };
        dbContext.Categories.Add(cat2);
        await dbContext.SaveChangesAsync();

        var art3 = new Article { CategoryId = cat2.Id, Title = "Devon Top Resort", Content = "Sunday Times readers voted Devon as their favourite holiday destination." };
        dbContext.Articles.Add(art3);
        await dbContext.SaveChangesAsync();
    }
}