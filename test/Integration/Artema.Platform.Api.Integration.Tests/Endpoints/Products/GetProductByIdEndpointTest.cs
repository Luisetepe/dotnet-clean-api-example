using System.Net;
using Artema.Platform.Api.Endpoints.GetProductById;
using Artema.Platform.Infrastructure.Data.DbContexts;
using FastEndpoints;
using FastEndpoints.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

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
        var productId = dbContext.Products.First().Id;

        var (httpRes, response) = await Fixture.Client
            .GETAsync<GetProductByIdEndpoint, GetProductByIdRequest, GetProductByIdResponse>(new()
        {
            Id = productId
        });

        httpRes.IsSuccessStatusCode.ShouldBeTrue();
        response.ShouldNotBeNull();
        response.Id.ShouldBe(productId);
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