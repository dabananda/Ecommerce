using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Api.Entities.OrderAggregate
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        public string BuyerId { get; set; } = string.Empty;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal GetTotal()
        {
            return Subtotal;
        }
    }
}