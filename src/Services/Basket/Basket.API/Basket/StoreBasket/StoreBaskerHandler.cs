namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommmandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommmandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not br null");
        RuleFor(x=>x.Cart.UserName).NotEmpty().WithMessage("UserName is required !");
    }
}

public class StoreBaskerHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }
}