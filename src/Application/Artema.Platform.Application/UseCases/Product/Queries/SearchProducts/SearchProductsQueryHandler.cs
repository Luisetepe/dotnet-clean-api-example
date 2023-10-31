using Artema.Platform.Domain.Criteria;
using Artema.Platform.Domain.Repositories;
using MediatR;

namespace Artema.Platform.Application.UseCases.Queries.SearchProducts;

public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, SearchProductsQueryResponse>
{
    private readonly IProductRepository _productRepository;

    public SearchProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<SearchProductsQueryResponse> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.SearchProducts(request.Criteria, cancellationToken);
        
        return new SearchProductsQueryResponse
        {
            Products = result.Select(p =>
                new SearchProductsQueryResponse.Product
                {
                    Id = p.Id.Value,
                    Name = p.Name.Value,
                    Pvp = p.Pvp.Value,
                    CategoryId = p.CategoryId?.Value,
                    CreatedAt = p.CreateDate
                }
            )
        };
    }
}