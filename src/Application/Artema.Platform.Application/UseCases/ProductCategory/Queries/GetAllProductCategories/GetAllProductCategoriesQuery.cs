using MediatR;

namespace Artema.Platform.Application.UseCases.Queries.GetAllProductCategories;

public record GetAllProductCategoriesQuery : IRequest<GetAllProductCategoriesQueryResponse>;
