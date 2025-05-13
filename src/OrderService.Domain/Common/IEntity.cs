namespace OrderService.Domain.Common
{
    public interface IEntity<T> : IEntity
    {
        public T Id { get; }
    }

    public interface IEntity
    {
        public DateTime? CreatedAt { get; }
        public string? CreatedBy { get; }
        public DateTime? LastModifiedAt { get; }
        public string? LastModifiedBy { get; }

        void SetAudit(string user, bool isNewlyAdded);
    }
}