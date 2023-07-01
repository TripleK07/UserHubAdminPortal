using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserHubAdminPortal.Models;
using UserHubAdminPortal.Helpers;

namespace UserHubAdminPortal.Controllers;

public class MenuController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MenuController> _logger;

    public MenuController(IHttpClientFactory httpClientFactory, ILogger<MenuController> logger)
    {
        _httpClient = httpClientFactory.CreateClient("UserHubAPI");
        _logger = logger;
    }

    public async Task<String> Index()
    {
        const string url = "api/v1/menu/GetAll";
        List<Menus>? menuList = await HTTPHelper<List<Menus>>.GetAPI(url, _httpClient);
        return JsonConvert.SerializeObject(menuList);
        //return PartialView("Menu", menuList);
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
