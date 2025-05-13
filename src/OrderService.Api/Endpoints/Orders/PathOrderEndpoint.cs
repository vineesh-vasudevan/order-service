using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.Commands.UpdateOrder;
using OrderService.Contracts.Models.Input;

namespace OrderService.Api.Endpoints.Orders
{
    public class PathOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapMethods("/orders/{orderId:guid}", ["PATCH"], PatchOrder)
               .Produces(StatusCodes.Status204NoContent)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .ProducesProblem(StatusCodes.Status404NotFound)
               .WithSummary("Update Order")
               .WithDescription("Updates the Order.");
        }

        private static async Task<IResult> PatchOrder(
            [FromRoute] Guid orderId,
            [FromBody] OrderPatchRequestDto request,
            [FromServices] ISender sender)
        {
            var command = new UpdateOrderCommand(orderId, request);
            await sender.Send(command);
            return Results.NoContent();
        }
    }
}