using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace CarRental.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);

        Task<TEntity> FindByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task RemoveAsync (TEntity entity);

        Task RemoveAsync(Guid id);

        ValueTask<TEntity> UpdateOneAsync(TEntity entity);

        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);
    }
}
