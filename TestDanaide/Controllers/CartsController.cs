using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TestDanaide.Models;
using TestDanaide.Persistence.Entities;
using TestDanaide.Services;
using TestDanaide.Services.Interfaces;

namespace TestDanaide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {

        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCartStatus(Guid? Id)
        {
            if(Id == null)
            {
                return BadRequest("Id is required");
            }
            Cart? cart = await _cartService.GetCartByIdAsync(Id.Value);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }
            GetCartStatusResponse cartResponse = new()
            {
                Total = cart.Total,
            };
            return Ok(cartResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest createCart)
        {
            if (ModelState.IsValid)
            {
                Result<Cart> cart = await _cartService.CreateCartAsync(createCart.DNI);
                if (cart.IsSuccess)
                {
                    CreateCartResponse createCartResponse = new CreateCartResponse
                    {
                        CartId = cart.Value.Id,
                    };
                    return Ok(createCartResponse);
                }
                else
                {
                    return BadRequest(cart.Errors);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartRequest addProductToCart)
        {
            if (ModelState.IsValid)
            {
                Result<Cart> result = await _cartService.AddProductToCart(addProductToCart.CartId, addProductToCart.ProductId);
                if (result.IsSuccess)
                {
                    AddProductToCartResponse addProductToCartResponse = new AddProductToCartResponse
                    {
                        Id = result.Value.Id,
                        Products = [.. result.Value.CartProducts.Select(x => new Product
                        {
                            Id = x.ProductId,
                            Name = x.Product.Name,
                            Price = x.Product.Price,
                        })],
                        Total = result.Value.Total,
                    };
                    return Ok(addProductToCartResponse);
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProductFromCart([FromBody] DeleteProductFromCartRequest deleteProductFromCart)
        {
            if (ModelState.IsValid)
            {
                Result<Cart> result = await _cartService.DeleteProductFromCart(deleteProductFromCart.CartId, deleteProductFromCart.ProductId);
                if (result.IsSuccess)
                {
                    DeleteProductFromCartResponse deleteProductFromCartResponse = new DeleteProductFromCartResponse
                    {
                        Products = [.. result.Value.CartProducts.Select(x => new Product
                        {
                            Id = x.ProductId,
                            Name = x.Product.Name,
                            Price = x.Product.Price,
                        })],
                        Total = result.Value.Total,
                    };
                    return Ok(deleteProductFromCartResponse);
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCart(Guid Id)
        {
            Result result = await _cartService.DeleteCartAsync(Id);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

    }
}
