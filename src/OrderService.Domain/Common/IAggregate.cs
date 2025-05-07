
namespace OrderService.Domain.Common
{
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {
    }

    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IReadOnlyList<IDomainEvent> ClearDomainEvents();
    }
}
