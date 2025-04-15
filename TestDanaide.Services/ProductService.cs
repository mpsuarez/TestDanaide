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
    public class ProductService : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<Result<Product>> CreateProductAsync(string name, decimal price)
        {
            try
            {
                var product = new Product
                {
                    Name = name,
                    Price = price,
                };
                await _productRepository.AddAsync(product);
                await _unitOfWork.CommitAsync();
                return Result.Ok(product);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("Error creating product").CausedBy(ex));
            }
        }

    }
}
