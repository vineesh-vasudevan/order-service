using Basket.CheckOutEvent;

namespace OrderService.Application.Orders.Commands.CreateOrder
{
    public interface ÌCreateOrderCommandFactory
    {
        CreateOrderCommand CreateOrderCommand(BasketCheckoutEvent basketCheckoutEvent);
    }
}