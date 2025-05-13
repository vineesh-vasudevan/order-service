using AutoMapper;
using FluentAssertions;
using OrderService.Application.Common.MappingProfiles;
using OrderService.Contracts.Models.Shared;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Tests.Common.MappingProfiles
{
    [TestFixture]
    public class AddressProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AddressProfile>();
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
        public void Should_Map_Address_To_AddressDto_Correctly()
        {
            // Arrange
            var address = new Address(
                FirstName: "John",
                LastName: "Doe",
                Street: "123 Main St",
                City: "Adliswil",
                State: "ZH",
                PostalCode: "8134",
                Country: "CH"
            );

            // Act
            var result = _mapper.Map<AddressDto>(address);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(address.FirstName);
            result.LastName.Should().Be(address.LastName);
            result.Street.Should().Be(address.Street);
            result.City.Should().Be(address.City);
            result.State.Should().Be(address.State);
            result.PostalCode.Should().Be(address.PostalCode);
            result.Country.Should().Be(address.Country);
        }
    }
}