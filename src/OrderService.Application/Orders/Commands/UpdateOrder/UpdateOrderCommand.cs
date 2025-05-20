namespace OrderService.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(Guid OrderId, OrderPatchRequestDto Request) : ICommand<bool>;
}