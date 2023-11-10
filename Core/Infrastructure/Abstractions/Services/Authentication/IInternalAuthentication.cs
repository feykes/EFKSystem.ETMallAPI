﻿using EFKSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task<DTOs.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
