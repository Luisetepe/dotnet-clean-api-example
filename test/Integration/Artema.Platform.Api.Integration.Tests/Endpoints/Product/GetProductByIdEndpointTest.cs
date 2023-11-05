using System.Net;
using Artema.Platform.Api.Endpoints.GetProductById;
using FastEndpoints;
using FastEndpoints.Testing;

namespace Artema.Platform.Api.Integration.Tests.Endpoints;

public class GetProductByIdEndpointTests : TestClass<EndpointTestFixture>
{
    public GetProductByIdEndpointTests(EndpointTestFixture s, ITestOutputHelper o) : base(s, o)
    {
    }

    [Fact]
    public async Task Given_ProductId_GetProductById_Endpoint_Should_Return_Expected_Product()
    {
        var dbContext = Fixture.GetDbContext();
        var product = dbContext.Products.First();

        var (httpRes, response) = await Fixture.Client
            .GETAsync<GetProductByIdEndpoint, GetProductByIdRequest, GetProductByIdResponse>(new()
        {
            Id = product.Id
        });

        httpRes.IsSuccessStatusCode.ShouldBeTrue();
        response.ShouldNotBeNull();
        response.Id.ShouldBe(product.Id);
        response.Name.ShouldBe(product.Name);
        response.Pvp.ShouldBe(product.Pvp);
        response.CategoryId.ShouldBe(product.CategoryId);
        response.CreatedAt.ShouldBe(product.CreatedAt);
    }

    [Fact]
    public async Task Given_NonExistent_ProductId_GetProductById_Endpoint_Should_Return_Null()
    {
        var (httpRes, _) = await Fixture.Client
            .GETAsync<GetProductByIdEndpoint, GetProductByIdRequest, GetProductByIdResponse>(new()
        {
            Id = Guid.NewGuid()
        });

        httpRes.IsSuccessStatusCode.ShouldBeFalse();
        httpRes.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
