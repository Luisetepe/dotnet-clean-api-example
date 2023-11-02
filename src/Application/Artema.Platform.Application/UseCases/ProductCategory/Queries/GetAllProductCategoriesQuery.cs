using MediatR;

namespace Artema.Platform.Application.UseCases.Queries;

public record GetAllProductCategoriesQuery : IRequest<GetAllProductCategoriesQueryResponse>;
