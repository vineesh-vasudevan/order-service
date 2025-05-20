namespace OrderService.Application.OrderItems.Commands.CreateOrderItem
{
    public record CreateOrderItemCommand(CreateOrderItemRequestDto Request, Guid OrderId) : ICommand<Guid>;
}