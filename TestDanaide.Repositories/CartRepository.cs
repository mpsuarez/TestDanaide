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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {

        private readonly IUnitOfWork _unitOfWork;

        public CartRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
