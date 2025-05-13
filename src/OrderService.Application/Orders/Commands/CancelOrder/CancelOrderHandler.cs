using OrderService.Domain.Exceptions;
using OrderService.Domain.Repositories;
using OrderService.Shared.CQRS;

namespace OrderService.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork) : ICommandHandler<CancelOrderCommand, bool>
    {
        // TODO: Replace with actual logged-in user
        private const string SystemUser = "System";

        public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var maybeOrder = await orderRepository.GetByIdAsync(command.Id, cancellationToken);

                if (maybeOrder.HasNoValue)
                {
                    throw new OrderNotFoundException(command.Id);
                }

                var order = maybeOrder.Value;
                order.Cancel(SystemUser);

                orderRepository.Update(order);
                await unitOfWork.CommitAsync(cancellationToken);

                return true;
            }
            catch
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}