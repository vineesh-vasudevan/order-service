using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.OrderItems.Commands.UpdateOrderItem;
using OrderService.Contracts.Models.Input;

namespace OrderService.Api.Endpoints.OrderItems
{
    public class PatchOrderItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapMethods("/orders/{orderId:guid}/items/{itemId:guid}", ["PATCH"], PatchOrderItem)
               .Produces(StatusCodes.Status204NoContent)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .ProducesProblem(StatusCodes.Status404NotFound)
               .WithSummary("Update Order Item")
               .WithDescription("Updates the quantity of a Order item.");
        }

        private static async Task<IResult> PatchOrderItem(
            [FromRoute] Guid orderId,
            [FromRoute] Guid itemId,
            [FromBody] OrderItemPatchRequestDto request,
            [FromServices] ISender sender)
        {
            var command = new UpdateOrderItemCommand(orderId, itemId, request);
            var result = await sender.Send(command);
            return Results.NoContent();
        }
    }
}