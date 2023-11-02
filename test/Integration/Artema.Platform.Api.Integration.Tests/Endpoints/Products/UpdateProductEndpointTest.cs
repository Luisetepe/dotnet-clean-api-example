using System.Net;
using Artema.Platform.Api.Endpoints.UpdateProduct;
using Artema.Platform.Api.Models;
using FastEndpoints;
using FastEndpoints.Testing;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Artema.Platform.Api.Integration.Tests.Endpoints;

public class UpdateProductEndpointTest : TestClass<EndpointTestFixture>
{
    public UpdateProductEndpointTest(EndpointTestFixture fixture, ITestOutputHelper output) : base(fixture, output) {}

    [Theory]
    [InlineData("Nvidia GeForce RTX 3080", 699)]
    [InlineData("AMD Radeon RX 6800 XT", 649)]
    [InlineData("Nvidia GeForce RTX 3090", 1499)]
    [InlineData("AMD Radeon RX 6900 XT", 999)]
    [InlineData("Nvidia GeForce RTX 3070", 499)]
    public async Task Given_Valid_Product_UpdateProduct_Endpoint_Should_Return_NoContent(string name, long pvp)
    {
        var product = await Fixture.GetDbContext().Products.AsNoTracking().FirstAsync();

        var httpRes = await Fixture.Client
            .PUTAsync<UpdateProductEndpoint, UpdateProductRequest>(new()
        {
            Id = product.Id,
            Name = name,
            Pvp = pvp,
            CategoryId =product.CategoryId
        });

        httpRes.IsSuccessStatusCode.ShouldBeTrue();
        httpRes.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var updatedProduct = await Fixture.GetDbContext().Products.AsNoTracking().FirstAsync(x => x.Id == product.Id);

        updatedProduct.Name.ShouldBe(name);
        updatedProduct.Pvp.ShouldBe(pvp);
        updatedProduct.Id.ShouldBe(product.Id);
        updatedProduct.CategoryId.ShouldBe(product.CategoryId);
    }

    [Theory]
    [InlineData("", 699)]
    [InlineData("Nvidia GeForce RTX 3080", 0)]
    public async Task Given_Invalid_Product_UpdateProduct_Endpoint_Should_Return_BadRequest(string name, long pvp)
    {
        var product = await Fixture.GetDbContext().Products.AsNoTracking().FirstAsync();

        var (httpRes, result) = await Fixture.Client
            .PUTAsync<UpdateProductEndpoint, UpdateProductRequest, ExceptionHttpResponse>(new()
        {
            Id = product.Id,
            Name = name,
            Pvp = pvp,
            CategoryId = product.CategoryId
        });

        httpRes.IsSuccessStatusCode.ShouldBeFalse();
        httpRes.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Type.ShouldBe("ValidationException");
    }

    [Fact]
    public async Task Given_NonExistent_ProductId_UpdateProduct_Endpoint_Should_Return_NotFound()
    {
        var httpRes = await Fixture.Client
            .PUTAsync<UpdateProductEndpoint, UpdateProductRequest>(new()
        {
            Id = Guid.NewGuid(),
            Name = "Nvidia GeForce RTX 3080",
            Pvp = 699,
            CategoryId = Guid.NewGuid()
        });

        httpRes.IsSuccessStatusCode.ShouldBeFalse();
        httpRes.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    // create test case for invalid category id
    [Fact]
    public async Task Given_Invalid_CategoryId_UpdateProduct_Endpoint_Should_Return_BadRequest()
    {
        var product = await Fixture.GetDbContext().Products.AsNoTracking().FirstAsync();

        var (httpRes, result) = await Fixture.Client
            .PUTAsync<UpdateProductEndpoint, UpdateProductRequest, ExceptionHttpResponse>(new()
        {
            Id = product.Id,
            Name = "Nvidia GeForce RTX 3080",
            Pvp = 699,
            CategoryId = Guid.NewGuid()
        });

        httpRes.IsSuccessStatusCode.ShouldBeFalse();
        httpRes.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Type.ShouldBe("RelationNotFoundException");
    }
}
