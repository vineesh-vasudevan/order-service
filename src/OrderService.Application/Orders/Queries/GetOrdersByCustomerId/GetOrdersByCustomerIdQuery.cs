using OrderService.Contracts.Models.Output;
using OrderService.Shared.CQRS;

namespace OrderService.Application.Orders.Queries.GetOrdersByCustomerId
{
    public record GetOrdersByCustomerIdQuery(Guid CustomerId) : IQuery<IEnumerable<OrderDto>>;
}
