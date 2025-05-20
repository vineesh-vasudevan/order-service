namespace OrderService.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(CreateOrderRequestDto Request) : ICommand<Guid>;
}