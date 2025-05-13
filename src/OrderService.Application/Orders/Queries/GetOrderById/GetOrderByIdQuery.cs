using OrderService.Contracts.Models.Output;
using OrderService.Shared.CQRS;

namespace OrderService.Application.Orders.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IQuery<OrderDto>;
}