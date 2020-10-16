using System;
using System.Linq;
using CarRental.Identity.EFCore;
using CarRental.Identity.Entities;

namespace CarRental.DAL.Repositories
{
    public class RefreshTokenRepository : IRepository<RefreshToken>
    {
        private readonly ApplicationIdentityContext _db;

        public RefreshTokenRepository(ApplicationIdentityContext db)
        {
            _db = db;
        }
        public void Create(RefreshToken item)
        {
            if (item != null)
            {
                _db.RefreshTokens.Add(item);
            }
            else
                throw new ArgumentNullException(nameof(item));

        }

        public void Delete(int id)
        {
            var item = _db.RefreshTokens.Find(id) ??
                       throw new Exception($"There is no token with id={id}");

            _db.RefreshTokens.Remove(item);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public RefreshToken Get(int id)
        {
            var item = _db.RefreshTokens.Find(id) ??
                       throw new Exception($"There is no token with id={id}");

            return item;
        }

        public IQueryable<RefreshToken> GetAll()
        {
            return _db.RefreshTokens.AsQueryable();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(RefreshToken item)
        {
            if (item != null)
            {
                _db.RefreshTokens.Update(item);
            }
            else
                throw new ArgumentNullException(nameof(item));
        }
    }
}
