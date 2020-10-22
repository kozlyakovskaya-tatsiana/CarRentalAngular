using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.DAL.EFCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ApplicationIdentityContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public EFGenericRepository(ApplicationIdentityContext context)
        {
            _context = context;

            _dbSet = context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Add(entity);
            }
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.ToArray();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public void Remove(TEntity entity)
        {
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            var entityToDelete = _dbSet.FirstOrDefault(el => el.Id == id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Update(entity);
            }
        }
    }
}
