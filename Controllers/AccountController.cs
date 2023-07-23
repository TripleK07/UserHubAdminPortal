using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserHubAdminPortal.Config;
using UserHubAdminPortal.Helpers;
using UserHubAdminPortal.Models;

namespace UserHubAdminPortal.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AccountController> _logger;
    private readonly string _apiUrl = "";

    public AccountController(ILogger<AccountController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        AppSettingsReader.GetAppSettings();
        _logger = logger;
        _apiUrl = AppSettingsReader.appSettings.ApiUrl;
        _httpClient = httpClientFactory.CreateClient(AppSettingsReader.appSettings.UserHubApi);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                LoginResponseModel? loginResponse = await HTTPHelper<LoginResponseModel>.SendAsync(_apiUrl + "api/v1/user/login", _httpClient, HttpMethod.Post, model);

                if (loginResponse != null)
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Sid, model.Username),
                    new Claim("Menus", JsonConvert.SerializeObject(loginResponse.Menus)),
                    new Claim("Token", loginResponse.Token),
                    // Add any additional claims as needed
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                        // Set any other authentication properties as needed
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );
                    // Redirect the user to the main page or a desired URL after successful login.
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unauthorized"))
                {
                    ModelState.AddModelError("errorMsg", "Invalid credentials. Please try again.");
                }
                else {
                    ModelState.AddModelError("errorMsg", "Something went wrong. Please try again.");
                }
            }
        }

        // If the ModelState is invalid, return the login view with the validation errors displayed.
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // Sign out the user using the authentication middleware.
        await HttpContext.SignOutAsync("Cookies");

        // Redirect the user to the main page or a desired URL after successful logout.
        return RedirectToAction("Login", "Account");
    }
}
