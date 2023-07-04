using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserHubAdminPortal.Models;
using UserHubAdminPortal.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace UserHubAdminPortal.Controllers;

[Authorize]
public class MenuController : Controller
{
    //private readonly HttpClient _httpClient;
    private readonly ILogger<MenuController> _logger;

    public MenuController(ILogger<MenuController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
