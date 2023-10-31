using MediatR;

namespace Artema.Platform.Application.UseCases.Commands.CreateProduct;

public record CreateProductCommand : IRequest<CreateProductCommandResponse>
{
    public string Name { get; init; } = default!;
    public long Pvp { get; init; }
    public Guid? CategoryId { get; init; }
}