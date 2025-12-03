using ECommerce.Api.Entities.OrderAggregate;

namespace ECommerce.Api.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByUserAsync(string buyerId);
        Task<Order?> GetOrderByIdAsync(Guid id, string buyerId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAdminAsync(Guid id);
        Task UpdateAsync(Order order);
    }
}
