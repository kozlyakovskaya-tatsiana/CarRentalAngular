using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarRental.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);

        Task<TEntity> FindByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAsync();

        Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate);

        void Remove (TEntity entity);

        Task RemoveAsync(Guid id);

        ValueTask<TEntity> UpdateOneAsync(TEntity entity);

        Task SaveChangesAsync();

        IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);
    }
}
