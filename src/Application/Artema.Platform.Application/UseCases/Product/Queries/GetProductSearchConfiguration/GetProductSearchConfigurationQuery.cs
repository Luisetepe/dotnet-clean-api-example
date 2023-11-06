using MediatR;

namespace Artema.Platform.Application.UseCases.Queries.GetProductSearchConfiguration;

public record GetProductSearchConfigurationQuery : IRequest<GetProductSearchConfigurationQueryResponse>;