using Artema.Platform.Application.UseCases.Queries;
using FastEndpoints;
using MediatR;

namespace Artema.Platform.Api.Endpoints.GetAllProductCategories;

public class GetAllProductCategoriesEndpoint : EndpointWithoutRequest<GetAllProductCategoriesResponse>
{
    private readonly ISender _sender;

    public GetAllProductCategoriesEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("api/product-categories");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllProductCategoriesQuery(), cancellationToken);

        await SendOkAsync(new GetAllProductCategoriesResponse
        {
            ProductCategories = result.ProductCategories.Select(x => new GetAllProductCategoriesResponse.ProductCategory
            {
                Id = x.Id,
                Name = x.Name,
                IsService = x.IsService,
            })
        },
        cancellationToken
    );
    }
}
