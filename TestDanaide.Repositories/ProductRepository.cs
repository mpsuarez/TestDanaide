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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
