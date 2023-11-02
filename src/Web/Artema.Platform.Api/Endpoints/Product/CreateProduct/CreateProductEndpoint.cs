using Artema.Platform.Api.Endpoints.GetProductById;
using Artema.Platform.Application.UseCases.Commands.CreateProduct;
using FastEndpoints;
using MediatR;

namespace Artema.Platform.Api.Endpoints.CreateProduct;

public class CreateProductEndpoint : Endpoint<CreateProductRequest, CreateProductResponse>
{
    private readonly ISender _sender;

    public CreateProductEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("/api/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductRequest request, CancellationToken ct)
    {
        var command = new CreateProductCommand
        {
            Name = request.Name,
            Pvp = request.Pvp,
            CategoryId = request.CategoryId
        };

        var result = await _sender.Send(command, ct);

        var response = new CreateProductResponse
        {
            Id = result.ProductData.Id,
            Name = result.ProductData.Name,
            Pvp = result.ProductData.Pvp
        };

        await SendCreatedAtAsync<GetProductByIdEndpoint>(new { Id = response.Id }, response, cancellation: ct);
    }
}
