using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserHubAdminPortal.Models;

namespace UserHubAdminPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var u = User.FindFirst(c=> c.Type == ClaimTypes.Sid)?.Value;
        var m = User.FindFirst(c=> c.Type == "Menus")?.Value;
        var t = User.FindFirst(c=> c.Type == "Token")?.Value;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
