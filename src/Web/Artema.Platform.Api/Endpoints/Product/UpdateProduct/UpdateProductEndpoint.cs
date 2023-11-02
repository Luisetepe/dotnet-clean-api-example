using Artema.Platform.Application.UseCases.Commands.UpdateProduct;
using FastEndpoints;
using MediatR;

namespace Artema.Platform.Api.Endpoints.UpdateProduct;

public class UpdateProductEndpoint : Endpoint<UpdateProductRequest>
{
    private readonly ISender _sender;

    public UpdateProductEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put("api/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send
        (
            new UpdateProductCommand
            {
                Id = request.Id,
                Name = request.Name,
                Pvp = request.Pvp,
                CategoryId = request.CategoryId
            },
            cancellationToken
        );

        await SendNoContentAsync(cancellationToken);
    }
}
