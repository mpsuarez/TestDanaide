﻿using FluentResults;
using TestDanaide.Persistence.Entities;

namespace TestDanaide.Services.Interfaces
{
    public interface IUserService
    {

        Task<Result<User>> CreateUserAsync(string DNI);

        Task<Result<IList<Product>>> GetMostExpensiveProductsBought(string DNI);

    }
}