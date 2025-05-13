using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Contracts.Models.Input;

namespace OrderService.Api.Endpoints.Orders
{
    public class CreateOrderEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", CreateOrder)
               .Produces<Guid>(StatusCodes.Status201Created)
               .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Order")
                .WithDescription("Create Order");
        }

        private static async Task<IResult> CreateOrder(
        CreateOrderRequestDto request,
        [FromServices] ISender sender)
        {
            var command = new CreateOrderCommand(request);
            var result = await sender.Send(command);
            return Results.Created($"/orders/{result}", result);
        }
    }
}