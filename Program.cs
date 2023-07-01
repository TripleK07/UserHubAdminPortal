var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//http interceptor
builder.Services.AddTransient<HttpInterceptor>();
builder.Services.AddHttpClient("UserHubAPI")
    .AddHttpMessageHandler<HttpInterceptor>();

//builder.Services.AddHttpClient();
// builder.Services.AddHttpClient("userHubAPI", httpClient =>
// {
//     httpClient.BaseAddress = new Uri("https://localhost:7014/api/");

//     // using Microsoft.Net.Http.Headers;
//     // The GitHub API requires two headers.
//     httpClient.DefaultRequestHeaders.Add(
//         HeaderNames.Accept, "application/json");
//     httpClient.DefaultRequestHeaders.Add(
//         HeaderNames.UserAgent, "HttpRequestsSample");
// });

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
