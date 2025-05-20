namespace OrderService.Contracts.Dto.Input
{
    public record OrderItemPatchRequestDto
    {
        public int Quantity { get; init; }
    }
}