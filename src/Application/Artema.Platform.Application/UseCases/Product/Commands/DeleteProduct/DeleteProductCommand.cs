using MediatR;

namespace Artema.Platform.Application.UseCases.Commands.DeleteProduct
{
    public record DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
