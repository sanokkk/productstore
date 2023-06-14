using UI.Service.Responses;
using UI.UI.Domain.Dto_S;

namespace UI.Service.Interfaces;

public interface IAuthService
{
    Task<IBaseReponse> RegisterAsync(RegisterDto model);

    Task<IBaseReponse> LoginAsync(LoginDto model);

    Task<IBaseReponse> GetAsync();
}