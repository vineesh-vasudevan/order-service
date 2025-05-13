namespace OrderService.Domain.Events
{
    public record OrderUpdatedEvent(Order Order) : IDomainEvent;
}