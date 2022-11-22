using DAL.Common;
using DAL.Entities;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repository
{
    public abstract class BaseReadonlyRepository<T> : IBaseReadonlyRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _entities;
        protected readonly AppDbContext _context;

        protected BaseReadonlyRepository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _entities;
        }

        public virtual T Find(int id)
        {
            return _entities.Find(id);
        }
    }
}
