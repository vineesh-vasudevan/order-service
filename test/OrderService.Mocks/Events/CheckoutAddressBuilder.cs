using Basket.CheckOutEvent;

namespace OrderService.Mocks.Events
{
    public class CheckoutAddressBuilder
    {
        private string _firstName = "John";
        private string _lastName = "Doe";
        private string _street = "123 Main St";
        private string _city = "New York";
        private string _state = "NY";
        private string _postalCode = "10001";
        private string _country = "USA";

        public CheckoutAddressBuilder WithFirstName(string firstName)
        { _firstName = firstName; return this; }

        public CheckoutAddressBuilder WithLastName(string lastName)
        { _lastName = lastName; return this; }

        public CheckoutAddressBuilder WithStreet(string street)
        { _street = street; return this; }

        public CheckoutAddressBuilder WithCity(string city)
        { _city = city; return this; }

        public CheckoutAddressBuilder WithState(string state)
        { _state = state; return this; }

        public CheckoutAddressBuilder WithPostalCode(string code)
        { _postalCode = code; return this; }

        public CheckoutAddressBuilder WithCountry(string country)
        { _country = country; return this; }

        public CheckoutAddress Build()
        {
            return new CheckoutAddress
            {
                FirstName = _firstName,
                LastName = _lastName,
                Street = _street,
                City = _city,
                State = _state,
                PostalCode = _postalCode,
                Country = _country
            };
        }

        public static CheckoutAddress Default() => new CheckoutAddressBuilder().Build();
    }
}