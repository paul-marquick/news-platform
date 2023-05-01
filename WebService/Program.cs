using Microsoft.EntityFrameworkCore;
using NewsPlatform.DataAccess;
using NewsPlatform.DataAccess.Seeding;
using NewsPlatform.WebService.Mappers;
using Serilog;

namespace NewsPlatform.WebService;

internal class Program
{
    private const string loggerOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} level={Level:w} msg={Message:lj} {NewLine}{Exception}";

    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .WriteTo.Console(outputTemplate: loggerOutputTemplate)
                .ReadFrom.Configuration(hostContext.Configuration);
        });

        builder.Services.AddDbContextPool<NewsPlatformDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NewsPlatform")));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<ICategoryMapper, CategoryMapper>();
        builder.Services.AddSingleton<IArticleMapper, ArticleMapper>();
        builder.Services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

        WebApplication app = builder.Build();

        // Use Open API UI in any environment. 
        app.UseSwagger();
        app.UseSwaggerUI();     
        
        app.MapControllers();

        app.Run();
    }
}