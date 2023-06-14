using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using UI.Service.Interfaces;
using UI.Service.Responses;
using UI.UI.Domain.Dto_S;

namespace UI.Providers;

public class CustomHttpHandler: DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;
    private readonly IHttpClientFactory _factory;
    private readonly AuthenticationStateProvider _provider;

    public CustomHttpHandler(ILocalStorageService localStorage, IHttpClientFactory factory, AuthenticationStateProvider provider)
    {
        _localStorage = localStorage;
        _factory = factory;
        _provider = provider;
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
        HttpResponseMessage originalResponse = null;
        try
        {
            originalResponse = await base.SendAsync(request, cancellationToken);
        }
        catch(Exception ex)
        {
            return await InvokeRefreshAPICall(originalResponse,request, token, cancellationToken); 
        }
        
        if (originalResponse.StatusCode == HttpStatusCode.Unauthorized)
        {
            return await InvokeRefreshAPICall(originalResponse,request, token, cancellationToken);
        }

        return originalResponse;
    }

    private async Task<HttpResponseMessage> InvokeRefreshAPICall(HttpResponseMessage originalResponse,
        HttpRequestMessage originalRequest,
        string jwtToken,
        CancellationToken cancellationToken)
    {
        var refreshToken = await _localStorage.GetItemAsync<string>("refresh-token");
        var userId = JwtHepler.ParseClaimsFromJwt(jwtToken).First(c => c.Type == "id").Value;
        
        var tokenRequest = new RefreshTokenRequestDto()
        {
            UserId = userId,
            RefreshToken = refreshToken
        };
        var content = JsonContent.Create(tokenRequest);
        
        var client = _factory.CreateClient("Sanokkk");
        var refreshResponse = await client.PostAsync("http://localhost:5243/api/Auth/Reauth", content);

        if (refreshResponse.StatusCode == HttpStatusCode.OK)
        {
            var newToken = await refreshResponse.Content.ReadFromJsonAsync<RenewTokenResponse>();
            await _localStorage.SetItemAsync("jwt-token", newToken.Token);
            await _localStorage.SetItemAsync("refresh-token", newToken.RefreshToken);
            (_provider as AuthProvider).NotifyAuthState();

            originalRequest.Headers.Remove("Authorization");
            originalRequest.Headers.Add("Authorization", $"Bearer {newToken.Token}");
            return await base.SendAsync(originalRequest, cancellationToken);
        }

        return originalResponse;
    }
    
    
}