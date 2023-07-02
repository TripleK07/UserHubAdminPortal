using System.Net.Http.Headers;

namespace UserHubAdminPortal.Config
{
    public class HttpInterceptor : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Perform interception logic before sending the request
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjRiM2NjZWYwLTZhZjctNDc4MC1hMjljLTBmYzAyMjlhYjk5MCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNjg4Mjc0MTIwLCJpc3MiOiJUcmlwbGVrJlltei5jb20iLCJhdWQiOiJvdXJQcm9qcyJ9.oRzzny1pR1hVnSXPe7_zv9ZeF-piWOLB0S9IAHA2kWo");

            // Call the inner handler to send the request
            _ = await base.SendAsync(request, cancellationToken);

            // Perform interception logic after receiving the response

            return await base.SendAsync(request, cancellationToken);
        }
    }
}