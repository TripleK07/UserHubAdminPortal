using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class HttpInterceptor : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Perform interception logic before sending the request
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjQ5MjE3NGE4LTM0ZDgtNGIzZS1hMjcxLTFkZTM3NzQyNDNmZSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNjg4MTQ0Mjc3LCJpc3MiOiJUcmlwbGVrJlltei5jb20iLCJhdWQiOiJvdXJQcm9qcyJ9.O15CXQmTcQp89D4ZHQ33p09D092H1XUhXt-g0fvz-pc");

        // Call the inner handler to send the request
        var response = await base.SendAsync(request, cancellationToken);

        // Perform interception logic after receiving the response

        return await base.SendAsync(request, cancellationToken);
    }
}
