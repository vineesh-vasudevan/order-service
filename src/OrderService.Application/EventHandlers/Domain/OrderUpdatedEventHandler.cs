using OrderService.Application.Common.Interfaces;

namespace OrderService.Application.EventHandlers.Domain
{
    public class OrderUpdatedEventHandler(
        IFeatureManager featureManager,
        IOrderEventPublisher orderEventPublisher,
        IMapper mapper,
        ILogger<OrderCreatedEventHandler> logger)
            : INotificationHandler<OrderUpdatedEvent>
    {
        public async Task Handle(OrderUpdatedEvent orderUpdatedEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", orderUpdatedEvent.GetType().Name);

            if (await featureManager.IsEnabledAsync("OrderEventPublishing"))
            {
                var orderDto = mapper.Map<OrderDto>(orderUpdatedEvent);
                await orderEventPublisher.PublishAsync(orderDto, cancellationToken);
            }
        }
    }
}