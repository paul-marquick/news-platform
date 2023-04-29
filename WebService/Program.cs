using Microsoft.EntityFrameworkCore;
using NewsPlatform.DataAccess;
using NewsPlatform.WebService.Mappers;

namespace NewsPlatform.WebService;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContextPool<NewsPlatformDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NewsPlatform")));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<ICategoryMapper, CategoryMapper>();
        builder.Services.AddSingleton<IArticleMapper, ArticleMapper>();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}