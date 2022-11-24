using DAL.Common;
using DAL.Entities;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL.Repository
{
    public abstract class BaseRepository<T> : BaseReadonlyRepository<T>, IBaseRepository<T> where T : BaseEntity
    {
        protected BaseRepository(AppDbContext context) : base (context)
        {
        }

        public virtual void Add(T entity)
        {
            _context.ChangeTracker.Clear();
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _context.ChangeTracker.Clear();
            _entities.AddRangeAsync(entities);
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _context.ChangeTracker.Clear();
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _context.ChangeTracker.Clear();
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
