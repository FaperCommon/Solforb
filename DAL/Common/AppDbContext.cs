using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Common
{
    public class AppDbContext : DbContext
    {
        internal DbSet<Order> Orders { get; set; }
        internal DbSet<OrderItem> OrderItems { get; set; }
        internal DbSet<Provider> Providers { get; set; }

        public AppDbContext(DbContextOptions options)
            : base(options)
        { }
    }
}
