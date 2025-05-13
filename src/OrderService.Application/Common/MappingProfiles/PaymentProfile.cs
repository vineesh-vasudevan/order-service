using AutoMapper;
using OrderService.Contracts.Models.Shared;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Common.MappingProfiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>();
        }
    }
}