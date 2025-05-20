namespace OrderService.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateOrderCommand, bool>
    {
        // TODO: Replace with actual logged-in user
        private const string SystemUser = "System";

        public async Task<bool> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var order = await GetOrderOrThrowAsync(command.OrderId, cancellationToken);

                ApplyOrderUpdates(order, command.Request);

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

        private async Task<Order> GetOrderOrThrowAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var maybeOrder = await orderRepository.GetByIdAsync(orderId, cancellationToken);

            if (maybeOrder.HasNoValue)
                throw new OrderNotFoundException(orderId);

            return maybeOrder.Value;
        }

        private void ApplyOrderUpdates(Order order, OrderPatchRequestDto request)
        {
            var updatedOrderName = string.IsNullOrEmpty(request.OrderName)
                ? order.OrderName
                : OrderName.Of(request.OrderName!);

            var updatedShippingAddress = CreateAddress(order.ShippingAddress, request.ShippingAddress);
            var updatedBillingAddress = CreateAddress(order.BillingAddress, request.BillingAddress);
            var updatedPayment = CreatePayment(order.Payment, request.Payment);

            var updatedStatus = request.Status == null
                ? order.Status
                : OrderStatus.FromName(request.Status.ToString(), ignoreCase: true);

            order.Update(
                updatedOrderName,
                updatedShippingAddress,
                updatedBillingAddress,
                updatedPayment,
                updatedStatus,
                SystemUser);
        }

        private static Address CreateAddress(Address existing, AddressDto? dto)
        {
            return dto is null
                ? existing
                : Address.Create(
                    dto.FirstName,
                    dto.LastName,
                    dto.Street,
                    dto.City,
                    dto.State,
                    dto.PostalCode,
                    dto.Country);
        }

        private static Payment CreatePayment(Payment existing, PaymentDto? dto)
        {
            return dto is null
                ? existing
                : Payment.Create(
                    dto.Amount,
                    Currency.Of(dto.Currency),
                    dto.PaidAt,
                    dto.PaymentMethod,
                    dto.IsSuccessful,
                    TransactionId.Of(dto.TransactionId));
        }
    }
}