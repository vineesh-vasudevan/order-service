using Basket.CheckOutEvent;

namespace OrderService.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandFactory(IMapper mapper) : ÌCreateOrderCommandFactory
    {
        // TODO: Replace with actual logged-in user
        private const string SystemUser = "System";

        public CreateOrderCommand CreateOrderCommand(BasketCheckoutEvent basketCheckoutEvent)
        {
            var orderId = basketCheckoutEvent.Id;

            var createOrderRequest = mapper.Map<CreateOrderRequestDto>(basketCheckoutEvent, opts =>
            {
                opts.Items["OrderId"] = orderId;
                opts.Items["CreatedBy"] = SystemUser;
            }) with
            {
                OrderItems = basketCheckoutEvent.Items
                   .Select(item => mapper.Map<CreateOrderItemRequestDto>((item, orderId, SystemUser)))
                   .ToList()
            };

            var command = new CreateOrderCommand(createOrderRequest);
            return command;
        }
    }
}