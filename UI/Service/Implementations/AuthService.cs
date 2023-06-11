using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using UI.Service.Interfaces;
using UI.Service.Responses;
using UI.UI.Domain.Dto_S;

namespace UI.Service.Implementations;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _factory;
    private Uri ControllerPath;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage, IHttpClientFactory factory)
    {
        _localStorage = localStorage;
        _factory = factory;
        ControllerPath = new Uri("http://localhost:5243/api/Auth");
        _httpClient = _factory.CreateClient("Sanokkk");
    }

    public async Task<IBaseReponse<string>> RegisterAsync(RegisterDto model)
    {
        var result = new RegisterResponse();

        var json = JsonContent.Create(model);

        var response = await _httpClient.PostAsync(ControllerPath, json);
        if (response.IsSuccessStatusCode)
        {
            result.StatusCode = HttpStatusCode.Created;
            result.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json").ToString();
        }
        else
        {
            result.StatusCode = response.StatusCode;
            result.Errors = JsonSerializer.Deserialize<List<string>>(await response?.Content?.ReadAsStringAsync());
            result.Success = false;
        }

        return result;
    }

    public async Task<IBaseReponse<string>> LoginAsync(LoginDto model)
    {
        var result = new LoginResponse();

        var Uri = new Uri(ControllerPath.ToString() + "/Login");

        var content = JsonContent.Create(model);

        var response = await _httpClient.PostAsync(Uri, content);
        if (response.IsSuccessStatusCode)
        {
            result.Content = (await response.Content.ReadFromJsonAsync<ApiAuthResponse>())?.Message;
        }
        else
        {
            result.Success = false;
        }

        return result;
    }

    public async Task<IBaseReponse<MyUser>> GetAsync()
    {

        var token = await _localStorage.GetItemAsync<string>("jwt-token");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", token);

        var response = await _httpClient.GetAsync(ControllerPath);

        var result = await response.Content.ReadFromJsonAsync<GetResponse>();
        return result;
    }
}