using FluentResults;
using TestDanaide.Persistence.Entities;

namespace TestDanaide.Services.Interfaces
{
    public interface ICartService
    {
        Task<Result<Cart>> AddProductToCart(Guid cartId, Guid productId);
        Task<Result<Cart>> CreateCartAsync(string DNI);
        Task<Result> DeleteCartAsync(Guid Id);
        Task<Result<Cart>> DeleteProductFromCart(Guid cartId, Guid productId);
        Task<Cart?> GetCartByIdAsync(Guid Id);
    }
}