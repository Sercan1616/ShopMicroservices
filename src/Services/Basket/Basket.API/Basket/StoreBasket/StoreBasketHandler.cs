namespace Basket.API.Basket.StoreBasket;

// Endpoint class'ındaki Request nesnesine karşılık gelen command nesnesi. Parametleri aynı.
public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

// Endpoint class'ındaki Response nesnesine karşılık gelen result nesnesi. Parametleri aynı.
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Cart must have a UserName");
    }
}
public class StoreBasketCommandHandler(IBasketRepository basketRepository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        await basketRepository.StoreBasket(cart, cancellationToken);

        return new StoreBasketResult(cart.UserName);
    }
}
