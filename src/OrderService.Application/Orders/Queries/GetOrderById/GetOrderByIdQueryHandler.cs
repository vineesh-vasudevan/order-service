using AutoMapper;
using OrderService.Contracts.Models.Output;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Repositories;
using OrderService.Shared.CQRS;

namespace OrderService.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        : IQueryHandler<GetOrderByIdQuery, OrderDto>
    {
        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByIdAsync(query.Id, cancellationToken);

            if (order.HasNoValue)
            {
                throw new OrderNotFoundException(query.Id);
            }

            return mapper.Map<OrderDto>(order.Value);
        }
    }
}