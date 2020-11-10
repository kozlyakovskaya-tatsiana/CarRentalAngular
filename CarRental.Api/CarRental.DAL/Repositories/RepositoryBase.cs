using CarRental.DAL.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarRental.DAL.Exceptions;
using Microsoft.EntityFrameworkCore.Query;

namespace CarRental.DAL.Repositories
{
    public class EfGenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationContext _context;

        protected readonly DbSet<TEntity> _dbSet;

        protected Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> _includesFunc;

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

                await _context.SaveChangesAsync();
            }
            else
                throw new Exception("Object is null. It can not be created.");
        }

        public async Task<TEntity> FindByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
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

        public async Task RemoveAsync(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);

                await _context.SaveChangesAsync();
            }
               
            else
                throw new Exception("Object is null. It can not be deleted.");
        }

        public async Task RemoveAsync(Guid id)
        {
            var entityToDelete = await _dbSet.FirstOrDefaultAsync(el => el.Id == id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);

                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException($"id: {id}");
        }

        public virtual async  ValueTask<TEntity> UpdateOneAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentOutOfRangeException(nameof(entity));

            var valTask = new ValueTask<TEntity>(_dbSet.Update(entity).Entity);

            await _context.SaveChangesAsync();

            return valTask.Result;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            return await Task.Run(() =>
            {
                var result = _dbSet.AsQueryable();

                if (includes != null)
                    result = includes(result);

                return result;
            });
        }
    }
}
