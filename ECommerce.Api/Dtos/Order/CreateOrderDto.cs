namespace ECommerce.Api.Dtos.Order
{
    public class CreateOrderDto
    {
        public AddressDto ShippingAddress { get; set; } = new();
    }
}
