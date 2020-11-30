using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Enums;
using CarRental.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CarRental.DAL
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationContext Context;

        protected readonly DbSet<TEntity> DbSet;

        protected Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> IncludesFunc;

        public RepositoryBase(ApplicationContext context)
        {
            Context = context;

            DbSet = context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (entity != null)
            {
                await DbSet.AddAsync(entity);

                await Context.SaveChangesAsync();
            }
            else
                throw new Exception("Object is null. It can not be created.");
        }

        public async Task<TEntity> FindByIdAsync(Guid id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
                throw new NotFoundException("There is no such object.");

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return(predicate != null) 
                 ? await DbSet.Where(predicate).ToArrayAsync()
                 : await DbSet.ToArrayAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            if (entity != null)
            {
                DbSet.Remove(entity);

                await Context.SaveChangesAsync();
            }
               
            else
                throw new Exception("Object is null. It can not be deleted.");
        }

        public async Task RemoveAsync(Guid id)
        {
            var entityToDelete = await DbSet.FirstOrDefaultAsync(el => el.Id == id);

            if (entityToDelete != null)
            {
                DbSet.Remove(entityToDelete);

                await Context.SaveChangesAsync();
            }
            else
                throw new NotFoundException($"id: {id}");
        }

        public virtual async ValueTask<TEntity> UpdateOneAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentOutOfRangeException(nameof(entity));

            var valTask = new ValueTask<TEntity>(DbSet.Update(entity).Entity);

            await Context.SaveChangesAsync();

            return valTask.Result;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            return await Task.Run(() =>
            {
                var result = predicate==null
                    ? DbSet.AsQueryable() 
                    : DbSet.Where(predicate).AsQueryable();

                if (includes != null)
                    result = includes(result);

                return result;
            });
        }
    }
}
