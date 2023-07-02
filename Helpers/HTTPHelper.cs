using System.Diagnostics;
using Newtonsoft.Json;
using UserHubAdminPortal.Config;

namespace UserHubAdminPortal.Helpers
{
    public static class HTTPHelper<T>
    {
        private const string API_URL = "https://localhost:7014/";
        private static readonly IHttpClientFactory _httpClientFactory;
        private static readonly IConfiguration _configuration;
        private static readonly string _apiName;
        static HTTPHelper()
        {
            // Build the configuration
             _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            // Create a new service collection
            var services = new ServiceCollection();

            //http interceptor
            services.AddTransient<HttpInterceptor>();

            // Read a value from the application.json file
            _apiName = _configuration["HttpClient:UserHubApi"];

            // Add the HttpClientFactory to the services collection
            services.AddHttpClient(_apiName).AddHttpMessageHandler<HttpInterceptor>();

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Get the IHttpClientFactory instance from the service provider
            _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }

        public static async Task<T?> GetAPI(string url)
        {
            HttpClient _httpClient = _httpClientFactory.CreateClient(_apiName);
            url = API_URL + url;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                // Process the response and return appropriate result
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    return default;
                }
            }
            catch
            {
                return default;
            }
        }
    }
}