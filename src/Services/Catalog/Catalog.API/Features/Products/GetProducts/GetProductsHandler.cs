
namespace Catalog.API.Features.Products.GetProducts;

public sealed record GetProductQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;

public sealed record GetProductResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
        return new GetProductResult(products);
    }
}
