using Artema.Platform.Api.Endpoints.SearchProducts;
using Artema.Platform.Api.Models;
using FastEndpoints;
using FastEndpoints.Testing;
using Shouldly;

namespace Artema.Platform.Api.Integration.Tests.Endpoints;

public class SearchProductsEndpointTest : TestClass<EndpointTestFixture>
{
    public SearchProductsEndpointTest(EndpointTestFixture s, ITestOutputHelper o) : base(s, o) {}

    [Fact]
    public async Task Given_Criteria_SearchProducts_Endpoint_Should_Return_Expected_Products()
    {
        // Test that we get 5 products with Pvp greater or equal than 2500.
        var (httpResByPvp, responseByPvp) = await Fixture.Client
            .POSTAsync<SearchProductsEndpoint, SearchProductsRequest, SearchProductsResponse>(new()
        {
            Filters = new []{ new SearchFilter{Field = "pvp", Value = "2500", Operator = "gte"} },
            OrderBy = "pvp",
            OrderType = "desc",
            Limit = 5
        });
    
        httpResByPvp.IsSuccessStatusCode.ShouldBeTrue();
        responseByPvp.Products.Count().ShouldBe(5);
        responseByPvp.Products.Count(x => x.Pvp >= 2500).ShouldBe(5);
        responseByPvp.Products.Count(x => x.Pvp < 2500).ShouldBe(0);

        // Test that we get 5 products with name containing the string 'on'.
        var (httpResByName, responseByName) = await Fixture.Client
            .POSTAsync<SearchProductsEndpoint, SearchProductsRequest, SearchProductsResponse>(new()
        {
            Filters = new []{ new SearchFilter{Field = "name", Value = "on", Operator = "inc"} },
            OrderBy = "name",
            OrderType = "asc",
            Limit = 3
        });

        httpResByName.IsSuccessStatusCode.ShouldBeTrue();
        responseByName.Products.Count().ShouldBe(3);
        responseByName.Products.Count(x => x.Name.Contains("on")).ShouldBe(3);
        responseByName.Products.Count(x => !x.Name.Contains("on")).ShouldBe(0);

        // Test that we get no product with name containing the string 'no_product_has_this_name'.
        var (httpResEmpty, responseEmpty) = await Fixture.Client
            .POSTAsync<SearchProductsEndpoint, SearchProductsRequest, SearchProductsResponse>(new()
        {
            Filters = new []{ new SearchFilter{Field = "name", Value = "no_product_has_this_name", Operator = "inc"} },
            OrderBy = "name",
            OrderType = "asc"
        });

        httpResEmpty.IsSuccessStatusCode.ShouldBeTrue();
        responseEmpty.Products.Count().ShouldBe(0);
    }
}