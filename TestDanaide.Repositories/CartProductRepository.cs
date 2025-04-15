using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities;
using TestDanaide.Repositories.Generics;
using TestDanaide.Repositories.Interfaces;

namespace TestDanaide.Repositories
{
    public class CartProductRepository : GenericRepository<CartProduct>, ICartProductRepository
    {

        private readonly IUnitOfWork _unitOfWork;

        public CartProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CartProduct?> GetCartProductByCartIdAndProductIdAsync(Guid cartId, Guid productId)
        {
            return await _unitOfWork.Context.CartProducts
                .SingleOrDefaultAsync(x => x.CartId == cartId && x.ProductId == productId);
        }

        public async Task<IList<Product>> GetMostExpensiveProductsBought(User user)
        {
            return await _unitOfWork.Context.Products
                .Where(p => p.CartProducts.Any(cp => cp.Cart.UserId == user.Id))
                .Take(4)
                .OrderByDescending(p => p.Price)
                .ToListAsync();
        }

        public async Task<CartProduct> CreateCartProduct(Guid cartId, Guid productId)
        {
            var cartProduct = new CartProduct
            {
                CartId = cartId,
                ProductId = productId
            };
            await _unitOfWork.Context.CartProducts.AddAsync(cartProduct);
            await _unitOfWork.CommitAsync();
            return cartProduct;
        }

    }
}
