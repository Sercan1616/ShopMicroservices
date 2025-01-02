
using Mapster;

namespace Basket.API.Basket.StoreBasket;

// Command nesnesine karşılık gelen request nesnesi. Parametleri aynı.
public record StoreBasketRequest(ShoppingCart Cart);

// Result nesnesine karşılık gelen response nesnesi. Parametleri aynı.
public record StoreBasketResponse(string UserName);
public class StoreBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {

            var command = request.Adapt<StoreBasketCommand>();

            // Command nesnesi üzerinden handler çağrılıyor.
            var result = await sender.Send(command);

            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{response.UserName}", response);
        }).
        WithName("CreateProduct")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
