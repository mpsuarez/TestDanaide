using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities;
using TestDanaide.Persistence.Enums;
using TestDanaide.Repositories;
using TestDanaide.Repositories.Generics;
using TestDanaide.Repositories.Interfaces;
using TestDanaide.Services.Interfaces;

namespace TestDanaide.Services
{
    public class CartService : ICartService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartProductRepository _cartProductRepository;
        private readonly bool _isSpecialDate;

        public CartService
        (
            IUnitOfWork unitOfWork,
            ICartRepository cartRepository,
            IUserRepository userRepository,
            IProductRepository productRepository,
            ICartProductRepository cartProductRepository
        )
        {
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _cartProductRepository = cartProductRepository;
            _isSpecialDate = false;
        }

        public async Task<Cart?> GetCartByIdAsync(Guid Id)
        {
            return await _cartRepository.GetByIdAsync(Id);
        }

        public async Task<Result<Cart>> CreateCartAsync(string DNI)
        {
            try
            {
                User? user = await _userRepository.GetUserByDNIAsync(DNI);
                if (user == null)
                {
                    return Result.Fail(new Error("User not found"));
                }

                CartType cartType;
                if (user.IsVip)
                {
                    cartType = CartType.Vip;
                }
                else if (_isSpecialDate)
                {
                    cartType = CartType.SpecialDate;
                }
                else
                {
                    cartType = CartType.Common;
                }

                var cart = new Cart
                {
                    UserId = user.Id,
                    Type = cartType,
                    CartProducts = [],
                    Total = 0
                };

                await _cartRepository.AddAsync(cart);
                await _unitOfWork.CommitAsync();

                return Result.Ok(cart);

            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("Error creating cart").CausedBy(ex));
            }
        }

        public async Task<Result<Cart>> AddProductToCart(Guid cartId, Guid productId)
        {
            try
            {
                Cart? cart = await _cartRepository.GetByIdAsync(cartId);
                if (cart == null)
                {
                    return Result.Fail(new Error("Cart not found"));
                }
                Product? product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    return Result.Fail(new Error("Product not found"));
                }
                CartProduct cartProduct = new()
                {
                    CartId = cart.Id,
                    ProductId = product.Id
                };
                await _cartProductRepository.CreateCartProduct(cartId, productId);

                // Calcula el precio total sin descuentos
                decimal baseTotal = cart.Total + product.Price;

                // Aplica el descuento
                decimal discount = CalculateDiscount(cart);

                cart.Total = baseTotal - discount;
                await _unitOfWork.CommitAsync();
                return Result.Ok(cart);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("Error adding product to cart").CausedBy(ex));
            }
        }

        public async Task<Result<Cart>> DeleteProductFromCart(Guid cartId, Guid productId)
        {
            try
            {
                Cart? cart = await _cartRepository.GetByIdAsync(cartId);
                if (cart == null)
                {
                    return Result.Fail(new Error("Cart not found"));
                }
                Product? product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    return Result.Fail(new Error("Product not found"));
                }
                CartProduct? cartProduct = await _cartProductRepository.GetCartProductByCartIdAndProductIdAsync(cartId, productId);
                if (cartProduct == null)
                {
                    return Result.Fail(new Error("Product not found in cart"));
                }
                cart.CartProducts.Remove(cartProduct);
                // Calcula el precio total sin descuentos
                decimal baseTotal = cart.Total - product.Price;

                // Aplica el descuento
                decimal discount = CalculateDiscount(cart);
                cart.Total = baseTotal - discount;
                await _unitOfWork.CommitAsync();
                return Result.Ok(cart);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("Error adding product to cart").CausedBy(ex));
            }
        }

        public async Task<Result> DeleteCartAsync(Guid Id)
        {
            try
            {
                var cart = await _cartRepository.GetAsync(x => x.Id == Id);
                if (cart == null)
                {
                    return Result.Fail(new Error("Cart not found"));
                }
                await _cartRepository.DeleteAsync(Id);
                await _unitOfWork.CommitAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("Error deleting cart").CausedBy(ex));
            }
        }

        private decimal CalculateDiscount(Cart cart)
        {
            int productCount = cart.CartProducts.Count;
            decimal discount = 0;

            if (productCount == 5)
            {
                discount = cart.Total * 0.2m;
            }

            else if (productCount > 10)
            {
                switch (cart.Type)
                {
                    case CartType.Common:
                        discount = 200;
                        break;
                    case CartType.SpecialDate:
                        discount = 500;
                        break;
                    case CartType.Vip:
                        var cheapestProduct = cart.CartProducts
                            .OrderBy(cp => cp.Product.Price)
                            .FirstOrDefault();

                        if (cheapestProduct != null)
                        {
                            discount = cheapestProduct.Product.Price + 700;
                        }
                        else
                        {
                            discount = 700;
                        }
                        break;
                }
            }

            return discount;
        }

    }
}
