namespace OrderService.Application.Orders.Queries.GetOrdersByCustomerId
{
    public record GetOrdersByCustomerIdQuery(Guid CustomerId) : IQuery<IEnumerable<OrderDto>>;
}