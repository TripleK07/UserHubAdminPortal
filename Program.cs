using Microsoft.AspNetCore.Authentication.Cookies;
using UserHubAdminPortal.Config;
using UserHubAdminPortal.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "UserInfo";
            options.Cookie.HttpOnly = true;
            options.LoginPath = "/Account/Login";
            // Configure other options as needed
        });

//http interceptor is registered in HTTPHelper.cs
builder.Services.AddTransient<UserHubAdminPortal.Config.HttpInterceptor>();

builder.Services.AddHttpClient("UserHubAPI")
    .AddHttpMessageHandler<UserHubAdminPortal.Config.HttpInterceptor>();

builder.Services.AddHttpContextAccessor();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();


