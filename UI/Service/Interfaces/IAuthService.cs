using UI.Service.Responses;
using UI.UI.Domain.Dto_S;

namespace UI.Service.Interfaces;

public interface IAuthService
{
    Task<IBaseReponse<string>> RegisterAsync(RegisterDto model);

    Task<IBaseReponse<string>> LoginAsync(LoginDto model);

    Task<IBaseReponse<MyUser>> GetAsync();
}