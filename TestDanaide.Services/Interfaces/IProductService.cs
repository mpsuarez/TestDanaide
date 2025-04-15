using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities;

namespace TestDanaide.Services.Interfaces
{
    public interface IProductService
    {

        Task<Result<Product>> CreateProductAsync(string name, decimal price);

    }
}
