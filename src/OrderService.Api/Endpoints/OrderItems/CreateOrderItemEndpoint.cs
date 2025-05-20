using OrderService.Application.OrderItems.Commands.CreateOrderItem;

namespace OrderService.Api.Endpoints.OrderItems
{
    public class CreateOrderItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders/{orderId:guid}/items", CreateOrderItem)
              .Produces<Guid>(StatusCodes.Status201Created)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Create Order Item")
              .WithDescription("Adds an item to the specified Order");
        }

        private static async Task<IResult> CreateOrderItem(
            [FromRoute] Guid orderId,
            CreateOrderItemRequestDto request,
            [FromServices] ISender sender)
        {
            var command = new CreateOrderItemCommand(request, orderId);
            var result = await sender.Send(command);
            return Results.Created($"/orders/{orderId}/items/{result}", result);
        }
    }
}