namespace OrderService.Application.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateOrderItemCommand>
    {
        // TODO: Replace with actual logged-in user
        private const string SystemUser = "System";

        public async Task<Unit> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
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

                order.UpdateOrderItem(command.OrderItemId, command.Request.Quantity, SystemUser);

                orderRepository.Update(order);
                await unitOfWork.CommitAsync(cancellationToken);
                return Unit.Value;
            }
            catch
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}