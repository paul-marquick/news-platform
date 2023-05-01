using Microsoft.AspNetCore.Mvc;
using NewsPlatform.DataAccess.Seeding;

namespace NewsPlatform.WebService.Controllers;

public class SeedController : ControllerBase
{
    private readonly IDatabaseSeeder databaseSeeder;
    private readonly ILogger<SeedController> logger;

    public SeedController(IDatabaseSeeder databaseSeeder, ILogger<SeedController> logger)
    {
        this.databaseSeeder = databaseSeeder;
        this.logger = logger;
    }

    [HttpPost("seed-database")]
    public async Task<ActionResult> SeedDatabase(IFormCollection collection)
    {
        await databaseSeeder.SeedDatabase();

        return NoContent();
    }
}