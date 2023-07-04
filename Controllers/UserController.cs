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
	private readonly string _apiUrl = "";

	public UserController(ILogger<UserController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
		_logger = logger;
		_apiUrl = AppSettingsReader.appSettings.ApiUrl;
		_httpClient = httpClientFactory.CreateClient(AppSettingsReader.appSettings.UserHubApi);
	}

    public async Task<IActionResult> Index()
    {
        string url = _apiUrl + "api/v1/user/GetAll";
        List<Users>? userList = await HTTPHelper<List<Users>>.SendAsync(url, _httpClient, HttpMethod.Get);
        return View(userList);
    }
}
