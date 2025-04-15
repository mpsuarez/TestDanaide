using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities;
using TestDanaide.Repositories.Generics;
using TestDanaide.Repositories.Interfaces;
using TestDanaide.Services.Interfaces;

namespace TestDanaide.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICartProductRepository _cartProductRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, ICartProductRepository cartProductRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _cartProductRepository = cartProductRepository;
        }

        public async Task<Result<User>> CreateUserAsync(string DNI)
        {
            try
            {
                User user = new User
                {
                    Dni = DNI,
                };
                await _userRepository.AddAsync(user);
                await _unitOfWork.CommitAsync();
                return Result.Ok(user);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("Error creating user").CausedBy(ex));
            }
        }

        public async Task<Result<IList<Product>>> GetMostExpensiveProductsBought(string DNI)
        {
            try
            {
                User? user = await _userRepository.GetUserByDNIAsync(DNI);
                if (user == null)
                {
                    return Result.Fail(new Error("User not found"));
                }
                var products = await _cartProductRepository.GetMostExpensiveProductsBought(user);
                if (products == null || products.Count == 0)
                {
                    return Result.Fail(new Error("No products found"));
                }
                return Result.Ok(products);

            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("Error getting products").CausedBy(ex));
            }
        }
    }
}
