using CarRental.DAL.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ApplicationContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public EFGenericRepository(ApplicationContext context)
        {
            _context = context;

            _dbSet = context.Set<TEntity>();
        }
        public async Task CreateAsync(TEntity entity)
        {
            if (entity != null)
            {
                await _dbSet.AddAsync(entity);
            }
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbSet.ToArrayAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run( () => _dbSet.Where(predicate).ToArray());
        }

        public async Task RemoveAsync(TEntity entity)
        {
            if (entity != null)
               await Task.Run(() =>  _dbSet.Remove(entity));
        }

        public async Task RemoveAsync(int id)
        {
            var entityToDelete = await _dbSet.FirstOrDefaultAsync(el => el.Id == id);

            if (entityToDelete != null)
            {
                await Task.Run( () => _dbSet.Remove(entityToDelete));
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity != null)
            {
                await Task.Run( () => _dbSet.Update(entity));
            }
        }
    }
}
