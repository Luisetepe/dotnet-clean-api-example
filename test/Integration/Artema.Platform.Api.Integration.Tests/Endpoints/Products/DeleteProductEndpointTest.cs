using Artema.Platform.Api.Endpoints.DeleteProduct;
using System.Net;
using FastEndpoints.Testing;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using FastEndpoints;
using Artema.Platform.Api.Models;

namespace Artema.Platform.Api.Integration.Tests.Endpoints;

public class DeleteProductEndpointTest : TestClass<EndpointTestFixture>
{
    public DeleteProductEndpointTest(EndpointTestFixture fixture, ITestOutputHelper output) : base(fixture, output){}

    [Fact]
    public async Task Given_Product_Id_DeleteProduct_Endpont_Should_Succeed()
    {
        var productId = await Fixture.GetDbContext().Products
            .Select(p => p.Id)
            .FirstOrDefaultAsync();

        var httpResponse = await Fixture.Client.DELETEAsync<DeleteProductEndpoint, DeleteProductRequest>(new DeleteProductRequest
        {
            Id = productId
        });

        httpResponse.IsSuccessStatusCode.ShouldBeTrue();

        var deleteProduct = await Fixture.GetDbContext().Products
            .SingleOrDefaultAsync(p => p.Id == productId);

        deleteProduct.ShouldBeNull();
    }

    [Fact]
    public async Task Given_Invalid_Product_Id_DeleteProduct_Endpont_Should_Return_NotFound()
    {
        var (httpResponse, result) = await Fixture.Client
            .DELETEAsync<DeleteProductEndpoint, DeleteProductRequest, ExceptionHttpResponse>(new DeleteProductRequest
        {
            Id = Guid.NewGuid()
        });

        httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        result.Type.ShouldBe("EntityNotFoundException");
    }

    [Fact]
    public async Task Given_Empty_Product_Id_DeleteProduct_Endpont_Should_Return_BadRequest()
    {
        var (httpResponse, result) = await Fixture.Client
            .DELETEAsync<DeleteProductEndpoint, DeleteProductRequest, ValidationExceptionResponse>(new DeleteProductRequest
        {
            Id = Guid.Empty
        });

        httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        result.Type.ShouldBe("ValidationException");
    }
}
