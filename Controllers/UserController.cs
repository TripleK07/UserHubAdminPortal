using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserHubAdminPortal.Models;
using UserHubAdminPortal.Helpers;

namespace UserHubAdminPortal.Controllers;

public class UserController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("UserHubAPI");
    }

    public async Task<IActionResult> Index()
    {
        const string url = "api/v1/user/GetAll";
        List<Users>? userList = await HTTPHelper<List<Users>>.Get(url, _httpClient);
        return View(userList);
    }
}
