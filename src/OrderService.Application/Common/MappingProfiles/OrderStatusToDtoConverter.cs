using AutoMapper;
using OrderService.Contracts.Models.Enums;
using OrderService.Domain.Enums;

namespace OrderService.Application.Common.MappingProfiles
{
    public class OrderStatusToDtoConverter : IValueConverter<OrderStatus, OrderStatusDto>
    {
        public OrderStatusDto Convert(OrderStatus sourceMember, ResolutionContext context)
        {
            return Enum.Parse<OrderStatusDto>(sourceMember.Name);
        }
    }
}