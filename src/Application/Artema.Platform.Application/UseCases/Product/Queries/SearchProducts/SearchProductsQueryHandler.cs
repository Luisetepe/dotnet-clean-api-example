using Artema.Platform.Application.Interfaces;
using MediatR;

namespace Artema.Platform.Application.UseCases.Queries.SearchProducts;

public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, SearchProductsQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public SearchProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SearchProductsQueryResponse> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var (products, totalResults) = await _unitOfWork.ProductRepository.SearchProducts(request.Criteria, true, cancellationToken);

        return new SearchProductsQueryResponse
        {
            Products = products.Select(p =>
                new SearchProductsQueryResponse.Product
                {
                    Id = p.Id.Value,
                    Name = p.Name.Value,
                    Pvp = p.Pvp.Value,
                    CategoryId = p.CategoryId?.Value,
                    CreatedAt = p.CreatedAt
                }
            ),
            TotalResults = totalResults
        };
    }
}
