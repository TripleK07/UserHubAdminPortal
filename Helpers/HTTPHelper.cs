using System.Diagnostics;
using Newtonsoft.Json;

namespace UserHubAdminPortal.Helpers
{
    public static class HTTPHelper<T>
    {
        private const string API_URL = "https://localhost:7014/";

        public static async Task<T?> GetAPI(string url, HttpClient httpClient)
        {
            HttpClient _httpClient = httpClient;
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