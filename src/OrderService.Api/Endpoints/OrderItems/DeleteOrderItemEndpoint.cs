using OrderService.Application.OrderItems.Commands.CancelOrderItem;

namespace OrderService.Api.Endpoints.OrderItems
{
    public class DeleteOrderItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{orderId:guid}/items/{itemId:guid}", CancelOrderItem)
              .Produces(StatusCodes.Status204NoContent)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .WithSummary("Cancel Order Item")
              .WithDescription("Removes an order item from the specified Order");
        }

        private static async Task<IResult> CancelOrderItem(
            [FromRoute] Guid orderId,
            [FromRoute] Guid itemId,
            [FromServices] ISender sender)
        {
            var command = new CancelOrderItemCommand(orderId, itemId);
            await sender.Send(command);
            return Results.NoContent();
        }
    }
}