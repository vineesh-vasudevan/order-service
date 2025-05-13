using AutoMapper;
using OrderService.Contracts.Models.Shared;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Common.MappingProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>();
        }
    }
}