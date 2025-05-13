namespace OrderService.Domain.Enums
{
    public class OrderItemStatus : SmartEnum<OrderItemStatus>
    {
        public static readonly OrderItemStatus Active = new(nameof(Active), 0);
        public static readonly OrderItemStatus Cancelled = new(nameof(Cancelled), 1);

        private OrderItemStatus(string name, int value) : base(name, value)
        {
        }
    }
}