using Basket.CheckOutEvent;

namespace OrderService.Application.Common.MappingProfiles
{
    public class PaymentDtoProfile : Profile
    {
        public PaymentDtoProfile()
        {
            CreateMap<CheckoutPayment, PaymentDto>();
        }
    }
}