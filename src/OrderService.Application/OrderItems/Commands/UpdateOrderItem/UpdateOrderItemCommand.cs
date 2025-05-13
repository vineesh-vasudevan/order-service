using OrderService.Contracts.Models.Input;
using OrderService.Shared.CQRS;

namespace OrderService.Application.OrderItems.Commands.UpdateOrderItem
{
    public record UpdateOrderItemCommand(Guid OrderId, Guid OrderItemId, OrderItemPatchRequestDto Request) : ICommand;
}