
namespace OrderService.Domain.Common
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; protected set; }
        public DateTime? CreatedAt { get; protected set; }
        public string? CreatedBy { get; protected set; }
        public DateTime? LastModifiedAt { get; protected set; }
        public string? LastModifiedBy { get; protected set; }        
    }
}
