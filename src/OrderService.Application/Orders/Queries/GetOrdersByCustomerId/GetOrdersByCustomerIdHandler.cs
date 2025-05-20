namespace OrderService.Application.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdHandler(IOrderRepository orderRepository, IMapper mapper)
        : IQueryHandler<GetOrdersByCustomerIdQuery, IEnumerable<OrderDto>>
    {
        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByCustomerIdQuery query, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetByCustomerIdAsync(query.CustomerId, cancellationToken);

            if (orders == null || !orders.Any())
                return Enumerable.Empty<OrderDto>();

            var orderDtos = mapper.Map<IEnumerable<OrderDto>>(orders);

            return orderDtos;
        }
    }
}