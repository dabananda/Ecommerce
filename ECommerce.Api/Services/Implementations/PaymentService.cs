using ECommerce.Api.Entities.OrderAggregate;
using ECommerce.Api.Repositories.Interfaces;
using ECommerce.Api.Services.Interfaces;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using Stripe;

namespace ECommerce.Api.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly StripeSettings _stripeSettings;
        private readonly IOrderRepository _orderRepo;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IOptions<StripeSettings> stripeSettings,
            IOrderRepository orderRepo,
            ILogger<PaymentService> logger)
        {
            _stripeSettings = stripeSettings.Value;
            _orderRepo = orderRepo;
            _logger = logger;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        public async Task<Order> CreateOrUpdatePaymentIntentAsync(Guid orderId)
        {
            var order = await _orderRepo.GetOrderByIdAdminAsync(orderId);

            if (order == null) throw new Exception("Order not found");

            if (order.Status == OrderStatus.PaymentReceived || order.Status == OrderStatus.Complete)
                throw new Exception("Order is already paid");

            var service = new PaymentIntentService();
            PaymentIntent intent;

            var amount = (long)(order.GetTotal() * 100);

            if (string.IsNullOrEmpty(order.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                    Metadata = new Dictionary<string, string>
                    {
                        { "OrderId", order.Id.ToString() }
                    }
                };
                intent = await service.CreateAsync(options);

                order.PaymentIntentId = intent.Id;
                order.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                intent = await service.UpdateAsync(order.PaymentIntentId, options);
            }

            await _orderRepo.UpdateAsync(order);
            return order;
        }

        public async Task UpdateOrderPaymentStatus(string paymentIntentId, OrderStatus status)
        {
            var order = await _orderRepo.GetOrderByPaymentIntentIdAsync(paymentIntentId);

            if (order == null)
            {
                _logger.LogWarning("Webhook received for unknown PaymentIntentId: {PaymentIntentId}", paymentIntentId);
                return;
            }

            if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Complete)
            {
                _logger.LogInformation("Order {OrderId} is already {Status}. Ignoring payment status update.", order.Id, order.Status);
                return;
            }

            if (order.Status != status)
            {
                order.Status = status;
                await _orderRepo.UpdateAsync(order);
                _logger.LogInformation("Order {OrderId} status updated to {Status}", order.Id, status);
            }
        }
    }
}