
namespace OrderService.Domain.Common
{
    public abstract class Entity<T> : IEntity<T>
    {
        public required T Id { get; init; }
        public DateTime? CreatedAt { get; init; }
        public string? CreatedBy { get; init; }
        public DateTime? LastModifiedAt { get; init; }
        public string? LastModifiedBy { get; init; }
    }
}
