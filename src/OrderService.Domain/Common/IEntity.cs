
namespace OrderService.Domain.Common
{
    public interface IEntity<T> : IEntity
    {
        public T Id { get; init; }
    }

    public interface IEntity
    {
        public DateTime? CreatedAt { get; init; }
        public string? CreatedBy { get; init; }
        public DateTime? LastModifiedAt { get; init; }
        public string? LastModifiedBy { get; init; }
    }
}
