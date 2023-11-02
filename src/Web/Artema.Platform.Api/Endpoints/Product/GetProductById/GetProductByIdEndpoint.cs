using Artema.Platform.Application.UseCases.Queries.GetProductById;
using FastEndpoints;
using MediatR;

namespace Artema.Platform.Api.Endpoints.GetProductById;

public class GetProductByIdEndpoint : Endpoint<GetProductByIdRequest, GetProductByIdResponse>
{
    private readonly ISender _sender;

    public GetProductByIdEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("/api/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductByIdRequest request, CancellationToken ct)
    {
        var result = await _sender.Send(new GetProductByIdQuery{Id = request.Id}, ct);

        await SendOkAsync(
            new GetProductByIdResponse
            {
                Id = result.ProductData.Id,
                Name = result.ProductData.Name,
                Pvp = result.ProductData.Pvp,
                CategoryId = result.ProductData.CategoryId,
                CreatedAt = result.ProductData.CreatedAt
            },
            cancellation: ct
        );
    }
}
