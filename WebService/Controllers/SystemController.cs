using Microsoft.AspNetCore.Mvc;

namespace NewsPlatform.WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class SystemController : ControllerBase
{
    private readonly ILogger<SystemController> logger;
    private readonly IWebHostEnvironment webHostEnvironment;

    public SystemController(IWebHostEnvironment webHostEnvironment, ILogger<SystemController> logger)
    {
        this.logger = logger;
        this.webHostEnvironment = webHostEnvironment;
    }

    [HttpGet("environment")]
    public ActionResult<string> GetEnvironment()
    {
        logger.LogDebug($"Environment is {webHostEnvironment.EnvironmentName}");

        return Ok(webHostEnvironment.EnvironmentName);
    }

    [HttpGet("echo")]
    public ActionResult<string> GetEcho(string text)
    {
        logger.LogDebug($"Echo text: {text}");

        return Ok(text);
    }
}