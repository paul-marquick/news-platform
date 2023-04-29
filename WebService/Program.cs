using Microsoft.EntityFrameworkCore;
using NewsPlatform.DataAccess;
using NewsPlatform.WebService.Mappers;

namespace NewsPlatform.WebService;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContextPool<NewsPlatformDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NewsPlatform")));

        builder.Services.AddSingleton<ICategoryMapper, CategoryMapper>();

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