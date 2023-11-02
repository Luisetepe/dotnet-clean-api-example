using MediatR;

namespace Artema.Platform.Application.UseCases.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public long Pvp { get; set; }
    public Guid? CategoryId { get; set; }
}
