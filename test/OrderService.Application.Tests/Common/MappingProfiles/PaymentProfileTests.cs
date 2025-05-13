using AutoMapper;
using FluentAssertions;
using OrderService.Application.Common.MappingProfiles;
using OrderService.Contracts.Models.Shared;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Tests.Common.MappingProfiles
{
    [TestFixture]
    public class PaymentProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PaymentProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public void Payment_Should_Map_To_PaymentDto_Correctly()
        {
            // Arrange
            var payment = Payment.Create(
                amount: 129.98m,
                currency: Currency.USD,
                paidAt: DateTime.UtcNow,
                paymentMethod: "Credit Card",
                isSuccessful: true,
                transactionId: TransactionId.Of("TXN123456")
            );

            // Act
            var result = _mapper.Map<PaymentDto>(payment);

            // Assert
            result.Amount.Should().Be(payment.Amount);
            result.Currency.Should().Be(payment.Currency.Code);
            result.PaidAt.Should().Be(payment.PaidAt);
            result.PaymentMethod.Should().Be(payment.PaymentMethod);
            result.IsSuccessful.Should().Be(payment.IsSuccessful);
            result.TransactionId.Should().Be(payment.TransactionId.Value);
        }
    }
}