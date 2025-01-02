namespace Catalog.API.Features.Products.DeleteProduct;

public class DeleteProductCammandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCammandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");
    }
}
