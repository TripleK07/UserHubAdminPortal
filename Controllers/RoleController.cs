using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserHubAdminPortal.Models;
using UserHubAdminPortal.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace UserHubAdminPortal.Controllers;

[Authorize]
public class RoleController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RoleController> _logger;
    private readonly string _apiUrl = "";

    public RoleController(ILogger<RoleController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _logger = logger;
        _apiUrl = AppSettingsReader.appSettings.ApiUrl + "api/v1/role/";
        _httpClient = httpClientFactory.CreateClient(AppSettingsReader.appSettings.UserHubApi);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }
}