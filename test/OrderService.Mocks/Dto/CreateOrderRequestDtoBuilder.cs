using OrderService.Contracts.Dto.Input;
using OrderService.Contracts.Models.Shared;

namespace OrderService.Mocks.Dto
{
    public class CreateOrderRequestDtoBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Guid _customerId = Guid.NewGuid();
        private string _orderName = "Test Order";
        private string _createdBy = "admin";
        private List<CreateOrderItemRequestDto> _items = new();

        private AddressDto _shippingAddress = new AddressDto
        {
            FirstName = "John",
            LastName = "Doe",
            Street = "123 Main St",
            City = "Zurich",
            State = "ZH",
            PostalCode = "8000",
            Country = "Switzerland"
        };

        private AddressDto _billingAddress = new AddressDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            Street = "456 Market St",
            City = "Bern",
            State = "BE",
            PostalCode = "3000",
            Country = "Switzerland"
        };

        private PaymentDto _payment = new PaymentDto
        {
            Amount = 100m,
            Currency = "CHF",
            PaidAt = DateTime.UtcNow,
            PaymentMethod = "CreditCard",
            IsSuccessful = true,
            TransactionId = "TXN-123456"
        };

        public CreateOrderRequestDtoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public CreateOrderRequestDtoBuilder WithCustomerId(Guid customerId)
        {
            _customerId = customerId;
            return this;
        }

        public CreateOrderRequestDtoBuilder WithOrderName(string orderName)
        {
            _orderName = orderName;
            return this;
        }

        public CreateOrderRequestDtoBuilder WithShippingAddress(AddressDto address)
        {
            _shippingAddress = address;
            return this;
        }

        public CreateOrderRequestDtoBuilder WithBillingAddress(AddressDto address)
        {
            _billingAddress = address;
            return this;
        }

        public CreateOrderRequestDtoBuilder WithPayment(PaymentDto payment)
        {
            _payment = payment;
            return this;
        }

        public CreateOrderRequestDtoBuilder WithOrderItems(List<CreateOrderItemRequestDto> items)
        {
            _items = items;
            return this;
        }

        public CreateOrderRequestDtoBuilder WithCreatedBy(string createdBy)
        {
            _createdBy = createdBy;
            return this;
        }

        public CreateOrderRequestDto Build()
        {
            return new CreateOrderRequestDto
            {
                Id = _id,
                CustomerId = _customerId,
                OrderName = _orderName,
                ShippingAddress = _shippingAddress,
                BillingAddress = _billingAddress,
                Payment = _payment,
                OrderItems = _items,
                CreatedBy = _createdBy,
                Status = Contracts.Dto.Enums.OrderStatusDto.Pending
            };
        }
    }
}