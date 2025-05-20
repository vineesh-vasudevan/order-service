using Basket.CheckOutEvent;

namespace OrderService.Application.Common.MappingProfiles
{
    public class AddressDtoProfile : Profile
    {
        public AddressDtoProfile()
        {
            CreateMap<CheckoutAddress, AddressDto>();
        }
    }
}