using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestDanaide.Models;
using TestDanaide.Services.Interfaces;

namespace TestDanaide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProduct)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.CreateProductAsync(createProduct.Name, createProduct.Price);
                if (product.IsSuccess)
                {
                    CreateProductResponse createProductResponse = new CreateProductResponse
                    {
                        ProductId = product.Value.Id,
                    };
                    return Ok(createProductResponse);
                }
                else
                {
                    return BadRequest(product.Errors);
                }
            }
            return BadRequest("Invalid model");
        }

    }
}
