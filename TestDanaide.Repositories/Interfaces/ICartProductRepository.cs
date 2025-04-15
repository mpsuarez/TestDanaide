using TestDanaide.Persistence.Entities;
using TestDanaide.Repositories.Generics;

namespace TestDanaide.Repositories.Interfaces
{
    public interface ICartProductRepository : IGenericRepository<CartProduct>
    {
        Task<CartProduct?> GetCartProductByCartIdAndProductIdAsync(Guid cartId, Guid productId);
        Task<IList<Product>> GetMostExpensiveProductsBought(User user);
        Task<CartProduct> CreateCartProduct(Guid cartId, Guid productId);
    }
}