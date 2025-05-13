using OrderService.Contracts.Models.Input;
using OrderService.Shared.CQRS;

namespace OrderService.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(CreateOrderRequestDto CreateOrderRequest) : ICommand<Guid>;
}