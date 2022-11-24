using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Common;
using DAL.Repository;
using DAL.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace IoC
{
    public class DependencyInjection
    {
        private readonly IConfiguration Configuration;
        public DependencyInjection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void InjectDependencies(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProviderService, ProviderService>();
        }
    }
}
