using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence;

namespace TestDanaide.Repositories.Generics
{
    public interface IUnitOfWork : IDisposable
    {

        TestDanaideDbContext Context { get; }

        Task CommitAsync();

    }
}
