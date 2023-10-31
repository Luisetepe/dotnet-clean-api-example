using NodaTime;

namespace Artema.Platform.Application.UseCases.Commands.CreateProduct;

public record CreateProductCommandResponse
{
    public record Product
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public long Pvp { get; init; }
        public Guid? CategoryId { get; init; }
        public Instant CreatedAt { get; init; }
    }

    public Product ProductData { get; init; } = default!;
}
