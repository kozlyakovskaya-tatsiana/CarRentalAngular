using System;
using System.Collections.Generic;

namespace CarRental.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);

        TEntity FindById(int id);

        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

        void Remove(TEntity entity);

        void Remove(int id);

        void Update(TEntity entity);

        void SaveChanges();
    }
}
