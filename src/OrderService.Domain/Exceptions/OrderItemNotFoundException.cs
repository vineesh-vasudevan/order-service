namespace OrderService.Domain.Exceptions
{
    public class OrderItemNotFoundException : NotFoundException
    {
        public OrderItemNotFoundException(Guid id)
            : base($"Order Item {id} was not found.")
        {
        }

        public OrderItemNotFoundException(OrderItemId id)
            : base($"Order Item {id.Value} was not found.")
        {
        }
    }
}