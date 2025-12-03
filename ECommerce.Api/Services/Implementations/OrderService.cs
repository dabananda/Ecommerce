using AutoMapper;
using ECommerce.Api.Dtos.Order;
using ECommerce.Api.Entities.OrderAggregate;
using ECommerce.Api.Repositories.Interfaces;
using ECommerce.Api.Services.Interfaces;

namespace ECommerce.Api.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepo, ICartRepository cartRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(string buyerId)
        {
            var cart = await _cartRepo.GetCartAsync(buyerId);

            if (cart == null || !cart.Items.Any())
                throw new Exception("Cannot checkout with an empty cart");

            var orderItems = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    PictureUrl = item.Product.Images.FirstOrDefault(x => x.IsMain)?.Url ?? "",
                    Price = item.Product.Price,
                    Quantity = item.Quantity
                };
                orderItems.Add(orderItem);
            }

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            var order = new Order
            {
                BuyerId = buyerId,
                OrderItems = orderItems,
                Subtotal = subtotal,
                Status = OrderStatus.Pending
            };

            await _orderRepo.CreateOrderAsync(order);

            await _cartRepo.DeleteCartAsync(buyerId);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id, string buyerId)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id, buyerId);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string buyerId)
        {
            var orders = await _orderRepo.GetOrdersByUserAsync(buyerId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}