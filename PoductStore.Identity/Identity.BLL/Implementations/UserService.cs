using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PoductStore.Identity.Identity.BLL.Dtos;
using PoductStore.Identity.Identity.BLL.Interfaces;
using PoductStore.Identity.Identity.BLL.Responses;
using PoductStore.Identity.Identity.DAL.Models;

namespace PoductStore.Identity.Identity.BLL.Implementations;

public class UserService: IUserService
{
    private readonly UserManager<User> _manager;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<UserManagerResponse> RegisterUserAsync(RegisterUserDto model)
    {
        if (model is null)
            throw new NullReferenceException("Model for registration is null");
        
        var User = _mapper.Map<User>(model);
        var response = new UserManagerResponse();
        
        var result = await _manager.CreateAsync(User, model.Password);
        if (result.Succeeded)
        {
            response.Message = "User created";
        }
        else
        {
            response.Success = false;
            response.Message = "User didnt create";
            response.Errors = result.Errors.Select(d => d.Description);
        }

        return response;
    }

    public List<string> GetUserNames()
    {
        var names = _manager.Users.Select(n => n.UserName).ToList();
        return names;
    }
    
    
}