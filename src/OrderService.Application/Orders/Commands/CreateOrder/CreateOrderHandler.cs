namespace OrderService.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<CreateOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var request = command.Request;

                var orderId = OrderId.Of(request.Id);

                var shippingAddress = Address.Create(
                    request.ShippingAddress.FirstName,
                    request.ShippingAddress.LastName,
                    request.ShippingAddress.Street,
                    request.ShippingAddress.City,
                    request.ShippingAddress.State,
                    request.ShippingAddress.PostalCode,
                    request.ShippingAddress.Country
                );

                var billingAddress = Address.Create(
                    request.BillingAddress.FirstName,
                    request.BillingAddress.LastName,
                    request.BillingAddress.Street,
                    request.BillingAddress.City,
                    request.BillingAddress.State,
                    request.BillingAddress.PostalCode,
                    request.BillingAddress.Country
                );

                var payment = Payment.Create(
                    request.Payment.Amount,
                    Currency.Of(request.Payment.Currency),
                    request.Payment.PaidAt,
                    request.Payment.PaymentMethod,
                    request.Payment.IsSuccessful,
                    TransactionId.Of(request.Payment.TransactionId)
                );

                var order = Order.Create(
                   orderId,
                   CustomerId.Of(request.CustomerId),
                   OrderName.Of(request.OrderName),
                   shippingAddress,
                   billingAddress,
                   payment
                );

                foreach (var item in request.OrderItems)
                {
                    var orderItem = OrderItem.Create(
                        OrderItemId.Of(item.Id),
                        orderId,
                        item.ProductCode,
                        item.Quantity,
                        item.UnitPrice,
                        item.TotalPrice
                    );

                    order.Add(orderItem);
                }

                await orderRepository.AddAsync(order, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);
                return order.Id.Value;
            }
            catch
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}