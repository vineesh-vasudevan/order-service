using OrderService.Application.Orders.Queries.GetOrderById;

namespace OrderService.Api.Endpoints.Orders
{
    public class GetOrdersByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{id}", GetOrder)
               .WithName("GetOrder")
               .WithSummary("Get Order By Id")
               .WithDescription("Get Order By Id")
               .Produces<OrderDto>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status404NotFound)
               .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<IResult> GetOrder(Guid id, ISender sender)
        {
            var result = await sender.Send(new GetOrderByIdQuery(id));
            return Results.Ok(result);
        }
    }
}