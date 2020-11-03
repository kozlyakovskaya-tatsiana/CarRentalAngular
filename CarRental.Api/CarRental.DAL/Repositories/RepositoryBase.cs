using CarRental.DAL.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.Exceptions;

namespace CarRental.DAL.Repositories
{
    public class EfGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ApplicationContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public EfGenericRepository(ApplicationContext context)
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
            else 
                throw new Exception("Object is null. It can not be created.");
        }

        public async Task<TEntity> FindByIdAsync(Guid id)
        {
            var entity =  await _dbSet.FindAsync(id);

            if(entity == null)
                throw new NotFoundException("There is no such object.");

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbSet.ToArrayAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> predicate)
        {
            return await _dbSet.Where(predicate).AsQueryable().ToArrayAsync();
        }

        public void Remove(TEntity entity)
        {
            if (entity != null)
                _dbSet.Remove(entity);
            else
                throw new Exception("Object is null. It can not be deleted.");
        }

        public async Task RemoveAsync(Guid id)
        {
            var entityToDelete = await _dbSet.FirstOrDefaultAsync(el => el.Id == id);

            if (entityToDelete != null)
            {
               _dbSet.Remove(entityToDelete);
            }
            else
                throw new NotFoundException("There is no object with such id.");
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual ValueTask<TEntity> UpdateOneAsync(TEntity entity)
        {
            if (entity == null)
                throw new Exception("Object is null. It can not be updated.");

            return new ValueTask<TEntity>(_dbSet.Update(entity).Entity);
        }
    }
}
