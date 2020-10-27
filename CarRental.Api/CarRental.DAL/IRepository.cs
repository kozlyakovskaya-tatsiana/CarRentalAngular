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

        Task<IEnumerable<TEntity>> Get(Func<TEntity, bool> predicate);

        Task Remove(TEntity entity);

        Task Remove(int id);

        Task Update(TEntity entity);

        Task SaveChanges();
    }
}
