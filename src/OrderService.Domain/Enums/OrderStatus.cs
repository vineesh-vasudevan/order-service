namespace OrderService.Domain.Enums
{
    public class OrderStatus : SmartEnum<OrderStatus>
    {
        public static readonly OrderStatus Pending = new(nameof(Pending), 0);
        public static readonly OrderStatus Confirmed = new(nameof(Confirmed), 1);
        public static readonly OrderStatus Shipped = new(nameof(Shipped), 2);
        public static readonly OrderStatus Delivered = new(nameof(Delivered), 3);
        public static readonly OrderStatus Cancelled = new(nameof(Cancelled), 4);

        private OrderStatus(string name, int value) : base(name, value) { }
    }
}
