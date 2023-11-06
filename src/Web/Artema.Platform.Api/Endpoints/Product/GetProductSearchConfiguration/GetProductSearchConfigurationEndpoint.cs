using Artema.Platform.Application.UseCases.Queries.GetProductSearchConfiguration;
using FastEndpoints;
using MediatR;

namespace Artema.Platform.Api.Endpoints.GetProductSearchConfiguration;

public class GetProductSearchConfigurationEndpoint : EndpointWithoutRequest<GetProductSearchConfigurationResponse>
{
    private readonly ISender _sender;

    public GetProductSearchConfigurationEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("/api/products/search-configuration");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetProductSearchConfigurationQuery(), cancellationToken);

        await SendOkAsync
        (
            new GetProductSearchConfigurationResponse
            {
                Endpoint = "/api/products/search",
                FilterFields = result.FilterFields,
                OrderByFields = result.OrderByFields,
            },
            cancellationToken
        );
    }
}