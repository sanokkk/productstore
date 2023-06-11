using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UI;
using UI.Providers;
using UI.Service.Implementations;
using UI.Service.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient()
// {
//     BaseAddress = new Uri("http://localhost:5165"),
// });

builder.Services.AddHttpClient("Sanokkk", client =>
{
    client.BaseAddress = new Uri("http://localhost:5165");
}).AddHttpMessageHandler<CustomHttpHandler>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CustomHttpHandler>();



await builder.Build().RunAsync();