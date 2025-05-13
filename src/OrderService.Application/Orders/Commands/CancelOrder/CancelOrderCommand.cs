using OrderService.Shared.CQRS;

namespace OrderService.Application.Orders.Commands.CancelOrder
{
    public record CancelOrderCommand(Guid Id) : ICommand<bool>;
}