using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserHubAdminPortal.Models;
using UserHubAdminPortal.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace UserHubAdminPortal.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserController> _logger;
	private readonly string _apiUrl = "";

	public UserController(ILogger<UserController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
		_logger = logger;
		_apiUrl = AppSettingsReader.appSettings.ApiUrl + "api/v1/user/";
		_httpClient = httpClientFactory.CreateClient(AppSettingsReader.appSettings.UserHubApi);
	}

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        //string url = _apiUrl + "GetAll";
        //List<Users>? userList = await HTTPHelper<List<Users>>.SendAsync(url, _httpClient, HttpMethod.Get);
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        string url = _apiUrl + "GetAll";
        List<Users>? userList = await HTTPHelper<List<Users>>.SendAsync(url, _httpClient, HttpMethod.Get);
        return PartialView("_List", userList);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid userId, bool isEdit)
    {
        if (isEdit)
        {
            string url = _apiUrl + "GetById/" + userId;
            Users? user = await HTTPHelper<Users>.SendAsync(url, _httpClient, HttpMethod.Get);
            return PartialView("_EditForm", user);
        }
        else 
        {
            return PartialView("_EditForm", new Users());
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(Users user)
    {
        string url = _apiUrl + "Update";
        try
        {
            await HTTPHelper<Users>.SendAsync(url, _httpClient, HttpMethod.Put, user);

            var msg = new { message = "Success", status = true };
            return Json(msg);
        }
        catch (Exception ex)
        {
            var msg = new { message = ex.Message, status = false };
            return Json(msg);
        }
    }
}
