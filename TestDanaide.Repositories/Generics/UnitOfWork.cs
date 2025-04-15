using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence;

namespace TestDanaide.Repositories.Generics
{
    public class UnitOfWork : IUnitOfWork
    {

        public TestDanaideDbContext Context { get; }

        public UnitOfWork(TestDanaideDbContext context)
        {
            Context = context;
        }

        public Task CommitAsync()
        {
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
