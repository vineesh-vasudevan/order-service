using Carter;
using MediatR;
using OrderService.Application.Orders.Commands.CancelOrder;

namespace OrderService.Api.Endpoints.Orders
{
    public class DeleteOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}", CancelOrder)
               .WithName("DeleteOrder")
               .Produces(StatusCodes.Status204NoContent)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .ProducesProblem(StatusCodes.Status404NotFound)
               .WithSummary("Delete Order")
               .WithDescription("Deletes a Order by its unique identifier.");
        }

        private static async Task<IResult> CancelOrder(Guid id, ISender sender)
        {
            var result = await sender.Send(new CancelOrderCommand(id));
            return Results.NoContent();
        }
    }
}