using AutoMapper;
using OrderService.Contracts.Models.Output;
using OrderService.Domain.Entities;

namespace OrderService.Application.Common.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(x => x.CustomerId, opt => opt.MapFrom(src => src.CustomerId.Value))
                .ForMember(x => x.OrderName, opt => opt.MapFrom(src => src.OrderName.Value))
                .ForMember(x => x.OrderItems, opt => opt.MapFrom(src => src.Items))
                .ForMember(x => x.Status, opt => opt.ConvertUsing(new OrderStatusToDtoConverter(), src => src.Status));
        }
    }
}