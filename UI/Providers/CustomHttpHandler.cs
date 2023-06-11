using Blazored.LocalStorage;

namespace UI.Providers;

public class CustomHttpHandler: DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public CustomHttpHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.RequestUri.AbsolutePath.ToLower().Contains("auth"))
        {
            return await base.SendAsync(request, cancellationToken);
        }
        
        var token = await _localStorage.GetItemAsync<string>("jwt-token");
        if (!string.IsNullOrEmpty(token))
            request.Headers.Add("Authorization", $"Bearer {token}");
        return await base.SendAsync(request, cancellationToken);

    }
}