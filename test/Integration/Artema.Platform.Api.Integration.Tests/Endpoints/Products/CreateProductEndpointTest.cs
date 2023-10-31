using System.Net;
using Artema.Platform.Api.Endpoints.CreateProduct;
using Artema.Platform.Api.Models;
using FastEndpoints;
using FastEndpoints.Testing;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Artema.Platform.Api.Integration.Tests.Endpoints;

public class CreateProductEndpointTest : TestClass<EndpointTestFixture>
{
    public CreateProductEndpointTest(EndpointTestFixture s, ITestOutputHelper o) : base(s, o)
    {
    }

    [Theory]
    [InlineData("Test Product", 1000)]
    [InlineData("Test-Product-2", 1)]
    public async Task Given_Valid_Product_CreateProduct_Endpoint_Should_Return_Expected_Product(string name, long pvp)
    {
        var dbContext = Fixture.GetDbContext();
        var categoryId = await dbContext.ProductCategories.Select(c => c.Id).FirstAsync();

        var (httpRes, response) = await Fixture.Client
            .POSTAsync<CreateProductEndpoint, CreateProductRequest, CreateProductResponse>(new()
            {
                Name = name,
                Pvp = pvp,
                CategoryId = categoryId
            });

        httpRes.IsSuccessStatusCode.ShouldBeTrue();
        response.ShouldNotBeNull();
        response.Name.ShouldBe(name);
        response.Pvp.ShouldBe(pvp);
    }

    [Theory]
    [InlineData("", 1000, null, "ValidationException")]
    [InlineData("Test Product", 0, null, "ValidationException")]
    [InlineData("Test Product", 1000, "f98ab6ec-b9d9-4419-881f-c690887d0fea", "RelationNotFoundException")]
    public async Task Given_Invalid_Product_CreateProduct_Endpoint_Should_Return_BadRequest
    (
        string name,
        long pvp,
        string? categoryId,
        string error
    )
    {
        var (httpRes, response) = await Fixture.Client
            .POSTAsync<CreateProductEndpoint, CreateProductRequest, ExceptionHttpResponse>(new()
            {
                Name = name,
                Pvp = pvp,
                CategoryId = categoryId is null ? null : Guid.Parse(categoryId)
            });

        httpRes.IsSuccessStatusCode.ShouldBeFalse();
        httpRes.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        response.Type.ShouldBe(error);
    }
}