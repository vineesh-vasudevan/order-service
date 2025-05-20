using Basket.CheckOutEvent;

using Microsoft.Extensions.Logging;
using OrderService.Application.Orders.Commands.CreateOrder;

namespace OrderService.Infrastructure.Messaging
{
    public class BasketCheckoutConsumer(ÌCreateOrderCommandFactory createOrderCommandFactory, ILogger<BasketCheckoutConsumer> logger, ISender sender) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var basketCheckoutEvent = context.Message!;
            logger.LogInformation($"BasketCheckout event received for Basket : {basketCheckoutEvent.BasketId} and Order {basketCheckoutEvent.Id}");

            var command = createOrderCommandFactory.CreateOrderCommand(basketCheckoutEvent);
            await sender.Send(command);
        }
    }
}