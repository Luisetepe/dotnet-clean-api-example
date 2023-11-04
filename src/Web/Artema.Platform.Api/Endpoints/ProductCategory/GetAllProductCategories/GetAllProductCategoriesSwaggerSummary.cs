using FastEndpoints;

namespace Artema.Platform.Api.Endpoints.GetAllProductCategories;

public class GetAllProductCategoriesSwaggerSummary : Summary<GetAllProductCategoriesEndpoint>
{
    public GetAllProductCategoriesSwaggerSummary()
    {
        Summary = "Gets all product categories";
        Response(200, "Returns all product categories", example: new GetAllProductCategoriesResponse
        {
            ProductCategories = new List<GetAllProductCategoriesResponse.ProductCategory>
            {
                new GetAllProductCategoriesResponse.ProductCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "Drinks",
                    IsService = false
                },
                new GetAllProductCategoriesResponse.ProductCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "Subscriptions",
                    IsService = true
                }
            }
        });
    }
}