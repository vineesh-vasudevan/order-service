namespace OrderService.Contracts.Models.Input
{
    public record OrderItemPatchRequestDto
    {
        public int Quantity { get; init; }
    }
}