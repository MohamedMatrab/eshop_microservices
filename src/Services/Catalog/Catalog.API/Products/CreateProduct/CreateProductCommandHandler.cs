namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required !");
        RuleFor(x=>x.Category).NotEmpty().WithMessage("Category is required !");
        RuleFor(x=>x.ImageFile).NotEmpty().WithMessage("ImageFile is required !");
        RuleFor(x=>x.Price).GreaterThan(0).WithMessage("price must be greater than zero !");
    }
}
internal class CreateProductCommandHandler(IDocumentSession session,ILogger<CreateProductCommandHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(CreateProductCommandHandler)}.{nameof(Handle)} called with {command} !");
        var product = new Product()
        {
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
            Name = command.Name
        };
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id); 
    }
}