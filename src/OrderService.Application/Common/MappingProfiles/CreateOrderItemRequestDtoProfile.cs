using Basket.CheckOutEvent;

namespace OrderService.Application.Common.MappingProfiles
{
    public class CreateOrderItemRequestDtoProfile : Profile
    {
        public CreateOrderItemRequestDtoProfile()
        {
            CreateMap<(BasketItemEvent basketItem, Guid orderId, string createdBy), CreateOrderItemRequestDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(src => src.basketItem.Id))
           .ForMember(d => d.ProductCode, opt => opt.MapFrom(src => src.basketItem.ProductCode))
           .ForMember(d => d.Quantity, opt => opt.MapFrom(src => src.basketItem.Quantity))
           .ForMember(d => d.UnitPrice, opt => opt.MapFrom(src => src.basketItem.Price))
           .ForMember(d => d.TotalPrice, opt => opt.MapFrom(src => src.basketItem.TotalPrice))
           .ForMember(d => d.OrderId, opt => opt.MapFrom(src => src.orderId))
           .ForMember(d => d.CreatedBy, opt => opt.MapFrom(src => src.createdBy));
        }
    }
}