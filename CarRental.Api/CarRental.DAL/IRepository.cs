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

        Task RemoveAsync(TEntity entity);

        Task RemoveAsync(int id);

        Task UpdateAsync(TEntity entity);

        Task SaveChangesAsync();
    }
}
