using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NewsPlatform.DTOs;

namespace NewsPlatform.WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class SystemController : ControllerBase
{
    private readonly ILogger<SystemController> logger;
    private readonly Config config;

    public SystemController(ILogger<SystemController> logger, IOptionsMonitor<Config> optionsMonitorConfig)
    {
        this.logger = logger;
        config = optionsMonitorConfig.CurrentValue;
    }

    [HttpGet("environment")]
    public ActionResult<EnvironmentData> GetEnvironmentData()
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;

        logger.LogDebug($"EnvironmentData, name: {environmentName}");

        var environmentData = new EnvironmentData
        {
            EnvironmentName = environmentName,
            WebsiteInstanceId = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"),
            WebsiteSiteName = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME")
        };

        return Ok(environmentData);
    }

    [HttpGet("echo")]
    public ActionResult<string> GetEcho(string text)
    {
        logger.LogDebug($"Echo text: {text}");

        return Ok(text);
    }

    [HttpGet("config")]
    public ActionResult<Config> GetConfig()
    {
        logger.LogDebug($"Config, Name: {config.Name},  FromEmailAddress: {config.FromEmailAddress},  WebServiceEnabled: {config.WebServiceEnabled}");

        return Ok(config);
    }
}