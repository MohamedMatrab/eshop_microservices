using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(IQuerySession session,ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetProductsQueryHandler)}.{nameof(Handle)} called with {query}");
        var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber,query.PageSize,cancellationToken);
        return new GetProductsResult(products);
    }
}