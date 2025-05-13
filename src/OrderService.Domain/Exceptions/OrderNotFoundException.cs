namespace OrderService.Domain.Exceptions
{
    public class OrderNotFoundException(Guid id) : NotFoundException($"Order {id} was not found.")
    {
    }
}