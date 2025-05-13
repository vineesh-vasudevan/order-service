using CSharpFunctionalExtensions;
using OrderService.Domain.Enums;
using OrderService.Domain.Repositories;

namespace OrderService.Infrastructure.Data.Repositories
{
    public class OrderRepository(OrderDbContext orderDbContext) : IOrderRepository
    {
        public async Task<Maybe<Order>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var order = await orderDbContext.Orders
                .Include(b => b.Items)
                .FirstOrDefaultAsync(
                    b => b.Id == OrderId.Of(id) && b.Status != OrderStatus.Cancelled,
                    cancellationToken);

            if (order == null)
                return Maybe<Order>.None;

            order.RemoveCancelledItems();

            return Maybe.From(order);
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            await orderDbContext.Orders.AddAsync(order, cancellationToken);
        }

        public void Update(Order order)
        {
            orderDbContext.Orders.Update(order);
        }
    }
}