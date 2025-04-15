using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities.Base;

namespace TestDanaide.Repositories.Generics
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity, IEntity
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Context.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> GetAsync(Expression<Func<T, bool>> whereCondition = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                  string includeProperties = "")
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>();

            if (whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<T?> AddAsync(T entity)
        {
            try
            {
                entity.Id = Guid.NewGuid();
                var save = await _unitOfWork.Context.Set<T>().AddAsync(entity);

                if (save != null)
                {
                    return entity;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        public async Task<T?> UpdateAsync(T entity)
        {
            try
            {
                _unitOfWork.Context.Set<T>().Update(entity);
                return await Task.FromResult(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T?> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Context.Set<T>().FindAsync(id);

                if (entity == null)
                {
                    return null;
                }

                _unitOfWork.Context.Set<T>().Remove(entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
