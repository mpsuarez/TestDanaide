using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities;
using TestDanaide.Repositories.Generics;

namespace TestDanaide.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
