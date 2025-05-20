using OrderService.Application.Common.Interfaces;

namespace OrderService.Infrastructure.Messaging
{
    public class OrderEventPublisher(IPublishEndpoint publishEndpoint) : IOrderEventPublisher
    {
        public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class
        {
            return publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}