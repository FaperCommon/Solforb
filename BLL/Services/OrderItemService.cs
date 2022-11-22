using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;

namespace BLL.Services
{
    public class OrderItemService : BaseService<OrderItemDTO, OrderItem>, IOrderItemService
    {
        public OrderItemService(IOrderItemRepository repository) : base(repository)
        {
        }
    }
}
