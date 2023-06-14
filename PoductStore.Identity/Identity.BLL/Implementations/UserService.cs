using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PoductStore.Identity.Identity.BLL.Dtos;
using PoductStore.Identity.Identity.BLL.Interfaces;
using PoductStore.Identity.Identity.BLL.Responses;
using PoductStore.Identity.Identity.DAL;
using PoductStore.Identity.Identity.DAL.Models;

namespace PoductStore.Identity.Identity.BLL.Implementations;

public class UserService: IUserService
{
    private readonly UserManager<User> _manager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly UsersDbContext _db;

    public UserService(UserManager<User> manager, IMapper mapper, IConfiguration configuration, UsersDbContext db)
    {
        _manager = manager;
        _mapper = mapper;
        _configuration = configuration;
        _db = db;
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
            expires:DateTime.Now.AddMinutes(3),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = await GenerateRefreshToken(user.Id);

        response.Message = tokenAsString;
        response.ExpireDate = token.ValidTo;
        response.RefreshToken = refreshToken;
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

    private async Task<string> GenerateRefreshToken(string userId)
    {
        var bytesOfToken = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytesOfToken);
        }

        var token = Convert.ToBase64String(bytesOfToken);

        var newRefresh = new UserRefreshToken()
        {
            ExpirationDate = DateTime.UtcNow.AddDays(3),
            Token = token,
            UserId = userId
        };

        _db.UserRefreshTokens.Add(newRefresh);
        await _db.SaveChangesAsync();

        return token;
    }

    public async Task<RenewTokenResponse> RenewTokenAsync(RenewTokenRequestDto request)
    {
        var existingRefreshToken = await _db.UserRefreshTokens
            .Where(_ => _.UserId == request.UserId &&
                        _.Token == request.RefreshToken &&
                        _.ExpirationDate > DateTime.UtcNow)
            .FirstOrDefaultAsync();

        if (existingRefreshToken is null)
        {
            return new RenewTokenResponse() { Success = false};
        }

        _db.UserRefreshTokens.Remove(existingRefreshToken);
        await _db.SaveChangesAsync();

        var user = await _manager.FindByIdAsync(request.UserId);

        var reloginResponse = await LoginAsync(_mapper.Map<LoginUserDto>(user));
        if (reloginResponse.Success)
        {
            return new RenewTokenResponse()
            {
                Token = reloginResponse.Message,
                RefreshToken = reloginResponse.RefreshToken
            };
        }
        else
        {
            return new RenewTokenResponse() { Success = false };
        }
    }
    
    
    
    
    
}