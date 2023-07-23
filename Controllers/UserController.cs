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
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        List<Users>? userList = new();
        try
        {
            string url = _apiUrl + "GetAll";
            userList = await HTTPHelper<List<Users>>.SendAsync(url, _httpClient, HttpMethod.Get);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return PartialView("_List", userList);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid userId, bool isEdit)
    {
        Users? user = new();
        if (isEdit)
        {
            try
            {
                string url = _apiUrl + "GetById/" + userId;
                user = await HTTPHelper<Users>.SendAsync(url, _httpClient, HttpMethod.Get);
                return PartialView("_EditForm", user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return PartialView("_EditForm", user);
            }
        }
        else
        {
            return PartialView("_EditForm", user);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(Users user)
    {
        try
        {
            string url = _apiUrl + "Update";
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
