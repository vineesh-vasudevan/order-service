namespace OrderService.Application.Common.Interfaces
{
    public interface IOrderEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
            where TEvent : class;
    }
}