using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using UI.Service.Interfaces;
using UI.Service.Responses;
using UI.UI.Domain.Dto_S;

namespace UI.Service.Implementations;

public class AuthService: IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly Uri ControllerPath;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        ControllerPath = new Uri("http://localhost:5243/api/Auth");
    }

    public async Task<IBaseReponse> RegisterAsync(RegisterDto model)
    {
        var result = new RegisterResponse();
        
        var json = JsonContent.Create(model);

        var response = await _httpClient.PostAsync(ControllerPath, json);
        if (response.IsSuccessStatusCode)
        {
            result.StatusCode = HttpStatusCode.Created;
        }
        else
        {
            result.StatusCode = response.StatusCode;
            result.Errors = JsonSerializer.Deserialize<List<string>>(await response?.Content?.ReadAsStringAsync());
            result.IsSuccess = false;
        }

        return result;
    }
}