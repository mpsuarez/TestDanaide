using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities.Base;

namespace TestDanaide.Repositories.Generics
{
    public interface IGenericRepository<T> where T : IEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<IList<T>> GetAsync(
            Expression<Func<T, bool>> whereCondition = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null,
            string includeProperties = ""
        );
        Task<T?> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<T?> DeleteAsync(Guid id);
    }
}
