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
        Task<bool> HasUserPurchasedProductAsync(string buyerId, Guid productId);
        Task<Order?> GetOrderByPaymentIntentIdAsync(string paymentIntentId);
        Task<List<Order>> GetExpiredOrdersAsync(DateTime olderThan);
    }
}
