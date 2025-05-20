using OrderService.Application.Common.Interfaces;

namespace OrderService.Application.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(
        IFeatureManager featureManager,
        IOrderEventPublisher orderEventPublisher,
        IMapper mapper,
        ILogger<OrderCreatedEventHandler> logger)
            : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent orderCreatedEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", orderCreatedEvent.GetType().Name);

            if (await featureManager.IsEnabledAsync("OrderEventPublishing"))
            {
                var orderCreatedIntegrationEvent = mapper.Map<OrderDto>(orderCreatedEvent);
                await orderEventPublisher.PublishAsync(orderCreatedIntegrationEvent, cancellationToken);
            }
        }
    }
}