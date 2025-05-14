using Carter;
using MediatR;
using OrderService.Application.Orders.Queries.GetOrdersByCustomerId;
using OrderService.Contracts.Models.Output;

namespace OrderService.Api.Endpoints.Orders
{
    public class GetOrdersByCustomerIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/customers/{customerId}/orders", GetOrdersByCustomer)
               .WithName("GetOrdersByCustomer")
                .Produces<IEnumerable<OrderDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Orders By Customer")
                .WithDescription("Get all orders placed by a specific customer.");
        }

        private static async Task<IResult> GetOrdersByCustomer(Guid customerId, ISender sender)
        {
            var result = await sender.Send(new GetOrdersByCustomerIdQuery(customerId));
            return Results.Ok(result);
        }
    }
}
