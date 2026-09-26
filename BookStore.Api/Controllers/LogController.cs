using Microsoft.AspNetCore.Mvc;
using BookStore.Application.Interfaces;
namespace BookStore.Controllers;
[ApiController]
[Route("[controller]")]
public class LogController:ControllerBase
    {
    private readonly ILogsService _logsService;
    public LogController(ILogsService logsService)
    {
        _logsService = logsService;
    }
    [HttpGet("log-information")]
    public IActionResult GetInformation() => Ok(_logsService.TriggerInfo());

    [HttpGet("log-debug")]
    public IActionResult GetDebug() => Ok(_logsService.TriggerDebug());

    [HttpGet("log-warning")]
    public IActionResult GetWarning() => Ok(_logsService.TriggerWarning());
    [HttpGet("log-error")]
    public IActionResult GetError() => Ok(_logsService.TriggerError());
}

