using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PoductStore.Identity.Identity.BLL.Dtos;
using PoductStore.Identity.Identity.BLL.Interfaces;
using PoductStore.Identity.Identity.BLL.Responses;
using PoductStore.Identity.Identity.DAL.Models;

namespace PoductStore.Identity.Identity.BLL.Implementations;

public class UserService: IUserService
{
    private readonly UserManager<User> _manager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<User> manager, IMapper mapper, IConfiguration configuration)
    {
        _manager = manager;
        _mapper = mapper;
        _configuration = configuration;
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

    public async Task<UserManagerResponse> LoginAsync(LoginUserDto model)
    {
        var response = new UserManagerResponse();
        var user = await _manager.FindByNameAsync(model.Username);

        if (user is null)
        {
            response.Message = "There is no user with this email";
            response.Success = false;
            return response;
        }

        var result = await _manager.CheckPasswordAsync(user, model.Password);

        if (!result)
        {
            response.Message = "Incorrect password";
            response.Success = false;
        }

        var claims = new[]
        {
            new Claim("username", model.Username),
            new Claim("id", user.Id),
            new Claim("email", user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));
        
        var token = new JwtSecurityToken(
            issuer:_configuration["AuthSettings:Issuer"],
            audience:_configuration["AuthSettings:Audience"],
            claims:claims,
            expires:DateTime.Now.AddHours(30),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

        response.Message = tokenAsString;
        response.ExpireDate = token.ValidTo;
        return response;
    }

    public List<string> GetUserNames()
    {
        var names = _manager.Users.Select(n => n.UserName).ToList();
        return names;
    }

    public async Task<GetUserResponse> GetUserAsync(string id)
    {
        var user = await _manager.FindByIdAsync(id);
        if (user is not null)
            return new GetUserResponse()
            {
                Content = UserForResponse.MapToDto(user)
            };
        else
            return new GetUserResponse()
            {
                Success = false
            };
    }
    
    
    
}