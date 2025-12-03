using ECommerce.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                var buyerId = User.Identity?.Name;
                if (string.IsNullOrEmpty(buyerId)) return Unauthorized();

                var order = await _orderService.CreateOrderAsync(buyerId);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId)) return Unauthorized();

            var orders = await _orderService.GetUserOrdersAsync(buyerId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId)) return Unauthorized();

            var order = await _orderService.GetOrderByIdAsync(id, buyerId);

            if (order == null) return NotFound();

            return Ok(order);
        }
    }
}