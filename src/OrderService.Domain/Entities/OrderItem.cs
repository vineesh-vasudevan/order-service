
namespace OrderService.Domain.Entities
{
    public class OrderItem : Entity<OrderItemId>
    {
        public OrderId OrderId { get; private set; } = default!;
        public string ProductCode { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
        public decimal UnitPrice { get; private set; } = default!;
        public decimal TotalPrice { get; private set; } = default!;

        private OrderItem(
           OrderItemId id,
           OrderId orderId,
           string productCode,
           int quantity,
           decimal unitPrice,
           decimal totalPrice,
           DateTime createdAt,
           DateTime lastModifiedAt,
           string createdBy,
           string lastModifiedBy)
        {
            if (id.Value == Guid.Empty)
                throw new ArgumentException("OrderItemId is required.", nameof(id));
            if (orderId.Value == Guid.Empty)
                throw new ArgumentException("OrderId is required.", nameof(orderId));
            if (string.IsNullOrWhiteSpace(productCode))
                throw new ArgumentException("ProductCode is required.", nameof(productCode));
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

            if (unitPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "UnitPrice cannot be negative.");


            if (totalPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(totalPrice), "TotalPrice cannot be negative.");

            Id = id;
            OrderId = orderId;
            ProductCode = productCode;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            CreatedAt = createdAt;
            LastModifiedAt = lastModifiedAt;
            CreatedBy = createdBy;
            LastModifiedBy = lastModifiedBy;
        }

        public static OrderItem Create(
           OrderItemId id,
           OrderId orderId,
           string productCode,
           int quantity,
           decimal unitPrice,
           decimal totalPrice,
           DateTime createdAt,
           DateTime lastModifiedAt,
           string createdBy,
           string lastModifiedBy)
        {
            return new(
                id,
                orderId,
                productCode,
                quantity,
                unitPrice,
                totalPrice,
                createdAt,
                lastModifiedAt,
                createdBy,
                lastModifiedBy);
        }
    }
}
