using MediatR;

namespace Artema.Platform.Application.UseCases.Queries.GetProductById;

public record GetProductByIdQuery : IRequest<GetProductByIdQueryResponse>
{
    public Guid Id { get; init; }    
};