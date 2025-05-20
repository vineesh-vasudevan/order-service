namespace OrderService.Application.OrderItems.Commands.CancelOrderItem
{
    public class CancelOrderItemHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<CancelOrderItemCommand, bool>
    {
        // TODO: Replace with actual logged-in user
        private const string SystemUser = "System";

        public async Task<bool> Handle(CancelOrderItemCommand command, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var maybeOrder = await orderRepository.GetByIdAsync(command.OrderId, cancellationToken);

                if (maybeOrder.HasNoValue)
                {
                    throw new OrderNotFoundException(command.OrderId);
                }

                var order = maybeOrder.Value;

                order.CancelOrderItem(command.OrderItemId, SystemUser);
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