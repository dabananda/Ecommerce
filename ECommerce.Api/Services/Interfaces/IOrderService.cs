using ECommerce.Api.Dtos.Order;

namespace ECommerce.Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(string buyerId);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string buyerId);
        Task<OrderDto?> GetOrderByIdAsync(Guid id, string buyerId);
    }
}
