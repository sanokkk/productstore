using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace UI.Providers;

public class AuthProvider:AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public AuthProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("jwt-token");

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        else
        {
            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(JwtHepler.ParseClaimsFromJwt(token), "JwtAuth")));
        }
    }
    
    public void NotifyAuthState()
    {
        NotifyAuthenticationStateChanged( GetAuthenticationStateAsync());
    }
}