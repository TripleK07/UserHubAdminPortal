using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserHubAdminPortal.Helpers;
using UserHubAdminPortal.Models;

namespace UserHubAdminPortal.Controllers;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("UserHubAPI");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        if (ModelState.IsValid)
        {
            LoginResponseModel? loginResponse = await HTTPHelper<LoginResponseModel>.Post("api/v1/user/login", model, _httpClient);

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
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
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

            // If the credentials are invalid, add a custom error message to the ModelState.
            ModelState.AddModelError("", "Invalid credentials. Please try again.");
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
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
