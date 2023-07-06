using System.Net.Http.Headers;
using System.Security.Claims;

namespace UserHubAdminPortal.Config
{
    public class HttpInterceptor : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Get the HttpContext from IHttpContextAccessor
            HttpContext? httpContext = _httpContextAccessor.HttpContext;
            string? token = httpContext?.User.FindFirst(c => c.Type == "Token")?.Value;
            if (token != null)
            {
                // Perform interception logic before sending the request
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Perform interception logic after receiving the response
            return await base.SendAsync(request, cancellationToken);
        }

        /*
		public async Task<HttpResponseMessage> GetAsync(string url)
		{
			using (var httpClient = new HttpClient(this))
			{
				return await httpClient.GetAsync(url);
			}
		}

		public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
		{
			using (var httpClient = new HttpClient(this))
			{
				return await httpClient.PostAsync(url, content);
			}
		}
        */
	}
}