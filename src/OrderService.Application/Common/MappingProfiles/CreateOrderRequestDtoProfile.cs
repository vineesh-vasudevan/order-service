using Basket.CheckOutEvent;

namespace OrderService.Application.Common.MappingProfiles
{
    public class CreateOrderRequestDtoProfile : Profile
    {
        public CreateOrderRequestDtoProfile()
        {
            CreateMap<BasketCheckoutEvent, CreateOrderRequestDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest, destMember, context) =>
                (Guid)context.Items["OrderId"]))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom((src, dest, destMember, context) =>
                context.Items["CreatedBy"]?.ToString() ?? "system"))
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore());
        }
    }
}