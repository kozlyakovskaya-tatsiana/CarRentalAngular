using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);

        Task<TEntity> FindByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAsync();

        Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate);

        void Remove (TEntity entity);

        Task RemoveAsync(int id);

        ValueTask<TEntity> UpdateOneAsync(TEntity entity);

        Task SaveChangesAsync();
    }
}
