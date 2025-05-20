namespace OrderService.Application.Orders.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IQuery<OrderDto>;
}