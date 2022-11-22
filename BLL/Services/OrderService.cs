using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;

namespace BLL.Services
{
    public class OrderService : BaseService<OrderDTO, Order>, IOrderService
    {
        public OrderService(IOrderRepository repository) : base(repository)
        {
        }
    }
}
