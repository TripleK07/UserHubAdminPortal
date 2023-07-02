using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using UserHubAdminPortal.Config;

namespace UserHubAdminPortal.Helpers
{
    public static class HTTPHelper<T>
    {
        private const string API_URL = "https://localhost:7014/";

        public static async Task<T?> Get(string url, HttpClient _httpClient)
        {
            url = API_URL + url;
            _httpClient.BaseAddress = new Uri(url);

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

        public static async Task<T?> Post(string url, Object requestEntity, HttpClient _httpClient)
        {
            // Append the API_URL to the provided URL
            url = API_URL + url;

            // Set the HttpClient's base address, headers, and content type
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serialize the request entity to JSON and create the request content
            using var content = new StringContent(JsonConvert.SerializeObject(requestEntity), UTF8Encoding.UTF8, "application/json");

            // Send the POST request and retrieve the response
            using var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                // If the response is successful, deserialize the response content to the specified entity type
                var objsJsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(objsJsonString);
            }
            else
            {
                // If the response is not successful, return the default value of the entity type
                return default;
            }
        }
    }
}