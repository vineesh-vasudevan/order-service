using AutoMapper;
using Basket.CheckOutEvent;
using FluentAssertions;
using OrderService.Application.Common.MappingProfiles;
using OrderService.Mocks.Events;

namespace OrderService.Application.Tests.Common.MappingProfiles
{
    [TestFixture]
    public class AddressDtoProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AddressDtoProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public void Should_have_valid_configuration()
        {
            //Assert
            _mapper
                .ConfigurationProvider
                .AssertConfigurationIsValid();
        }

        [Test]
        public void Should_Map_AddressDto_To_CheckoutAddress_Correctly()
        {
            //Arrange
            var source = CheckoutAddressBuilder.Default();

            // Act
            var result = _mapper.Map<CheckoutAddress>(source);

            //Assert

            result.Should().NotBeNull();
            result.FirstName.Should().Be(source.FirstName);
            result.LastName.Should().Be(source.LastName);
            result.Street.Should().Be(source.Street);
            result.City.Should().Be(source.City);
            result.State.Should().Be(source.State);
            result.PostalCode.Should().Be(source.PostalCode);
            result.Country.Should().Be(source.Country);
        }
    }
}