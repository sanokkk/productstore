using PoductStore.Identity.Identity.BLL.Dtos;
using PoductStore.Identity.Identity.BLL.Responses;

namespace PoductStore.Identity.Identity.BLL.Interfaces;

public interface IUserService
{
    Task<UserManagerResponse> RegisterUserAsync(RegisterUserDto model);

    List<string> GetUserNames();
}