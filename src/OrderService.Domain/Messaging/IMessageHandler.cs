namespace OrderService.Domain.Messaging
{
    public interface IMessageHandler<TMessage>
    {
        Task HandleAsync(TMessage message, CancellationToken cancellationToken);
    }
}