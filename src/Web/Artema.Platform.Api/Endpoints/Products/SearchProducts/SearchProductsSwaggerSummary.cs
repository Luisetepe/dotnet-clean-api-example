using Artema.Platform.Api.Models;
using FastEndpoints;

namespace Artema.Platform.Api.Endpoints.SearchProducts;

public class SearchProductsSwaggerSummary : Summary<SearchProductsEndpoint>
{
    public SearchProductsSwaggerSummary()
    {
        Summary = "Search products by a given criteria.";
        ExampleRequest = new SearchProductsRequest
        {
            Filters = new[] { new SearchFilter { Field = "pvp", Value = "2500", Operator = "gte" } },
            OrderBy = "pvp",
            OrderType = "desc",
            Limit = 5
        };
        Response(200, "A list of Products found by given criteria.", example: new SearchProductsResponse
        {
            Products = new []
            {
                new SearchProductsResponse.Product
                {
                    Id = Guid.NewGuid(),
                    Name = "CocaCola",
                    Pvp = 2600,
                    CategoryId = Guid.NewGuid()
                },
                new SearchProductsResponse.Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Doritos",
                    Pvp = 3000,
                    CategoryId = Guid.NewGuid()
                }
            }
        });
        Response(400, "Produced when the provided criteria is not valid.", "application/problem+json", example: new ValidationExceptionResponse
        {
            StatusCode = 400,
            Type = "ValidationException",
            Message = "One or more validation errors occurred",
            Errors = new Dictionary<string, string[]>
            {
                {"OrderBy", new [] { "Value must be null or not empty." }},
                {"Limit", new [] { "Value must be greater than 0." }}
            }
        });
    }
}
