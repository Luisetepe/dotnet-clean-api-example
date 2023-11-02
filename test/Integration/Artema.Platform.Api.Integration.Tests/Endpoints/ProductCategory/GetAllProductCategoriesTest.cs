using Artema.Platform.Api.Endpoints.ProductCategory.GetAllProductCategories;
using FastEndpoints;
using FastEndpoints.Testing;
using Microsoft.EntityFrameworkCore;

namespace Artema.Platform.Api.Integration.Tests.Endpoints;

public class GetAllProductCategoriesTest : TestClass<EndpointTestFixture>
{
    public GetAllProductCategoriesTest(EndpointTestFixture f, ITestOutputHelper o) : base(f, o)
    {
    }

    [Fact]
    public async Task Given_ProductCategories_GetAllProductCategories_Endpoint_Should_Succeed()
    {
        var categoriesCount = await Fixture.GetDbContext().ProductCategories.CountAsync();
        var (httpResponse, result) = await Fixture.Client.GETAsync<GetAllProductCategoriesEndpoint, GetAllProductCategoriesResponse>();

        httpResponse.IsSuccessStatusCode.ShouldBeTrue();
        result.ProductCategories.Count().ShouldBe(categoriesCount);
    }
}
