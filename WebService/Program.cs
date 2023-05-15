using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
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

        string? connectionString;

        if (IsAzure())
        {
            // In production get connection string from Azure key vault.
            Uri keyVaultEndpoint = new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/");
            SecretClient secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());

            KeyVaultSecret kvs = secretClient.GetSecret("NewsPlatformConnectionString");
            connectionString = kvs.Value;
        }
        else
        {
            connectionString = builder.Configuration.GetConnectionString("NewsPlatform")!;
        }

        builder.Services.AddDbContextPool<NewsPlatformDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<ICategoryMapper, CategoryMapper>();
        builder.Services.AddSingleton<IArticleMapper, ArticleMapper>();
        builder.Services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

        WebApplication app = builder.Build();

        // Use Open API UI in any environment, as this is just a demo. 
        app.UseSwagger();
        app.UseSwaggerUI();     
        
        app.MapControllers();

        app.Run();
    }

    #region Private 

    private static bool IsAzure()
    {
        return !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID")) &&
            !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));
    }

    #endregion 
}