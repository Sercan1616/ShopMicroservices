namespace Catalog.API.Features.Products.CreateProduct;

/// Endpoint'te send metodu handler'ı tetiklediğinde öncelikle bu method çalışır ve kuralları kontrol eder.

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is requeired");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is requeired");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is requeired");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
