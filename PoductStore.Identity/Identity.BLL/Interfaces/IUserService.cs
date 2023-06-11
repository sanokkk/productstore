﻿using PoductStore.Identity.Identity.BLL.Dtos;
using PoductStore.Identity.Identity.BLL.Responses;
using PoductStore.Identity.Identity.DAL.Models;

namespace PoductStore.Identity.Identity.BLL.Interfaces;

public interface IUserService
{
    Task<UserManagerResponse> RegisterUserAsync(RegisterUserDto model);

    List<string> GetUserNames();

    Task<UserManagerResponse> LoginAsync(LoginUserDto model);

    Task<GetUserResponse> GetUserAsync(string id);
}