namespace OrderService.Application.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<CreateOrderItemCommand, Guid>
    {
        public async Task<Guid> Handle(CreateOrderItemCommand command, CancellationToken cancellationToken)
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

                var orderItem = OrderItem.Create(
                    id: OrderItemId.Of(command.Request.Id),
                    orderId: order.Id,
                    productCode: command.Request.ProductCode,
                    quantity: command.Request.Quantity,
                    unitPrice: command.Request.UnitPrice,
                    totalPrice: command.Request.TotalPrice
                );

                order.Add(orderItem);

                orderRepository.Update(order);
                await unitOfWork.CommitAsync(cancellationToken);

                return orderItem.Id.Value;
            }
            catch
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}