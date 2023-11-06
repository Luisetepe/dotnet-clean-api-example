using FastEndpoints;

namespace Artema.Platform.Api.Endpoints.GetProductSearchConfiguration;

public class GetProductSearchConfigurationSwaggerSummary : Summary<GetProductSearchConfigurationEndpoint>
{
    public GetProductSearchConfigurationSwaggerSummary()
    {
        Summary = "Get product search configuration";
        Response(200, "Product search configuration", example: new GetProductSearchConfigurationResponse
        {
            Endpoint = "/api/products/search",
            FilterFields = new Dictionary<string, string[]>
            {
                { "Id", new[] { "eq", "neq" } },
                { "Name", new[] { "eq", "neq", "inc" } },
                { "Pvp", new[] { "eq", "neq", "gt", "gte", "lt", "lte" } },
                { "CategoryId", new[] { "eq", "neq" } },
                { "CreatedAt", new[] { "eq", "neq", "gt", "gte", "lt", "lte" } }
            },
            OrderByFields = new[] { "Name", "Pvp", "CreatedAt" },
        });
    }
}