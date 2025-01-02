namespace Catalog.API.Features.Products.CreateProduct;

public sealed record CreateProductRequest(string Name, string Description, decimal Price, string ImageFile, List<string> Category);

public sealed record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            // CreateProductRequest tipindeki request nesnesini, CreateProductCommand tipine dönüştürüyor.
            // Mapster özelliği
            var command = request.Adapt<CreateProductCommand>();

            // Handle metodu tetitler
            var result = await sender.Send(command);

            // Result nesnesini CreateProductResponse tipine dönüştürüyor.
            // Komut çalıştıktan sonra dönen sonucu, API yanıt formatına uyumlu hale getirir.
            var response = result.Adapt<CreateProductResponse>(); // Guid Id geriye döner

            return Results.Created($"/products/{response.Id}", response);
        })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
    }
}