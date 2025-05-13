using AutoMapper;
using OrderService.Contracts.Models.Enums;
using OrderService.Contracts.Models.Output;
using OrderService.Domain.Entities;

namespace OrderService.Application.Common.MappingProfiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(x => x.OrderId, opt => opt.MapFrom(src => src.OrderId.Value))
                .ForMember(x => x.OrderId, opt => opt.MapFrom(src => src.OrderId.Value))
                .ForMember(x => x.Quantity, opt => opt.MapFrom(src => src.Quantity.ToString()))
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(src => (OrderItemStatusDto)src.Status.Value))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(x => x.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy))
                .ForMember(x => x.LastModifiedAt, opt => opt.MapFrom(src => src.LastModifiedAt));
        }
    }
}