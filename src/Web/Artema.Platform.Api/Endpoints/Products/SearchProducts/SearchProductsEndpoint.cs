using Artema.Platform.Application.UseCases.Queries.SearchProducts;
using FastEndpoints;
using MediatR;

namespace Artema.Platform.Api.Endpoints.SearchProducts;

public class SearchProductsEndpoint : Endpoint<SearchProductsRequest, SearchProductsResponse>
{
    private readonly ISender _sender;

    public SearchProductsEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("/api/products/search");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SearchProductsRequest request, CancellationToken ct)
    {
        var criteria = request.ToCriteria();
        var result = await _sender.Send(new SearchProductsQuery{Criteria = criteria}, ct);

        await SendAsync(
            new SearchProductsResponse{
                Products = result.Products.Select(p => new SearchProductsResponse.Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Pvp = p.Pvp,
                    CategoryId = p.CategoryId,
                    CreateDate = p.CreatedAt
                })
            },
            cancellation: ct
        );
    }
}