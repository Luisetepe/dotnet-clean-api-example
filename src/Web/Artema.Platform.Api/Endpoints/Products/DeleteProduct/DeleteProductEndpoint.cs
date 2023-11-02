using Artema.Platform.Application.UseCases.Commands.DeleteProduct;
using FastEndpoints;
using MediatR;

namespace Artema.Platform.Api.Endpoints.DeleteProduct;

public class DeleteProductEndpoint : Endpoint<DeleteProductRequest>
{
    private readonly ISender _sender;

    public DeleteProductEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Delete("/api/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(new DeleteProductCommand{ Id = request.Id }, cancellationToken);

        await SendOkAsync();
    }
}
