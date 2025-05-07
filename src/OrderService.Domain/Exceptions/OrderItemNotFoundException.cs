
namespace OrderService.Domain.Exceptions
{
    public class OrderItemNotFoundException(OrderItemId id) : NotFoundException($"Order Item {id.Value} was not found.")
    {
    }
}
