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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> GetUserByDNIAsync(string DNI)
        {
            return await _unitOfWork.Context.Users.SingleOrDefaultAsync(x => x.Dni == DNI);
        }
    }
}
