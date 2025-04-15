
using TestDanaide.Persistence.Entities;
using TestDanaide.Repositories.Generics;

namespace TestDanaide.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByDNIAsync(string DNI);

    }
}