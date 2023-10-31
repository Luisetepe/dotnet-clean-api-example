using Artema.Platform.Domain.Criteria;
using MediatR;

namespace Artema.Platform.Application.UseCases.Queries.SearchProducts;

public record SearchProductsQuery : IRequest<SearchProductsQueryResponse>
{
    public SearchCriteria Criteria { get; init; } = default!;
}