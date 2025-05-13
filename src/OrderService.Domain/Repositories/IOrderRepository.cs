using CSharpFunctionalExtensions;

namespace OrderService.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Maybe<Order>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task AddAsync(Order order, CancellationToken cancellationToken);

        void Update(Order order);
    }
}