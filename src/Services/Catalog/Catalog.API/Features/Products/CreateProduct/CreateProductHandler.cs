namespace Catalog.API.Features.Products.CreateProduct;

public sealed record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category) : ICommand<CreateProductResult>;

public sealed record CreateProductResult(Guid Id);


internal class CreateProductCommandHandler(IDocumentSession session) :
    ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // 1- Create product entity from command object
        // 2- Save to database
        // 3- Return CreateProduckResult result


        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            ImageFile = command.ImageFile,
            Category = command.Category
        };

        // save to database

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // return result
        return new CreateProductResult(product.Id);
    }
}
