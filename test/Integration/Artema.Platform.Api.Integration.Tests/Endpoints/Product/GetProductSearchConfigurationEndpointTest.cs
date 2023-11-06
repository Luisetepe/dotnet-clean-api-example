using Artema.Platform.Api.Endpoints.GetProductSearchConfiguration;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Artema.Platform.Api.Integration.Tests.Endpoints;

public class GetProductSearchConfigurationEndpointTest : TestClass<EndpointTestFixture>
{
    public GetProductSearchConfigurationEndpointTest(EndpointTestFixture f, ITestOutputHelper o) : base(f, o){}

    [Fact]
    public async Task GetProductSearchConfigurationEndpoint_Should_Return_Expected_Configuration()
    {
        var expectedResult = new GetProductSearchConfigurationResponse
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
            OrderByFields = new List<string> { "Id", "Name", "Pvp", "CategoryId", "CreatedAt" },
        };


        var (httpRes, response) = await Fixture.Client
            .GETAsync<GetProductSearchConfigurationEndpoint, GetProductSearchConfigurationResponse>();

        httpRes.IsSuccessStatusCode.ShouldBeTrue();
        response.ShouldNotBeNull();
        response.Endpoint.ShouldBeEquivalentTo(expectedResult.Endpoint);
        response.FilterFields
            .Aggregate(true, (acc, next) => acc && next.Value.SequenceEqual(expectedResult.FilterFields[next.Key]))
            .ShouldBeTrue();
        response.OrderByFields.SequenceEqual(expectedResult.OrderByFields).ShouldBeTrue();
    }
}