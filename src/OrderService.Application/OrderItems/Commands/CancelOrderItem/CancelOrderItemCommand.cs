namespace OrderService.Application.OrderItems.Commands.CancelOrderItem
{
    public record CancelOrderItemCommand(Guid OrderId, Guid OrderItemId) : ICommand<bool>;
}