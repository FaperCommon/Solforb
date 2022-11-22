using DAL.Common;
using DAL.Entities;
using DAL.Repository.Interfaces;
using System.Collections.Generic;

namespace DAL.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public override void AddRange(IEnumerable<Order> entities)
        {
            foreach (var entity in _entities)
            {
                _entities.Remove(entity);
            }
            _entities.AddRangeAsync(entities);
            _context.SaveChanges();
        }
    }
}
