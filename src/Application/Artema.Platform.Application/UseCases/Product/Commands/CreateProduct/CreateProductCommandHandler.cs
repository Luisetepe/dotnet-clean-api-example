using Artema.Platform.Application.Interfaces;
using Artema.Platform.Domain.Entities;
using Artema.Platform.Domain.Repositories;
using MediatR;
using NodaTime;

namespace Artema.Platform.Application.UseCases.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IUniqueIdentifierGenerator _identifierGenerator;
    private readonly IClock _clock;

    public CreateProductCommandHandler
    (
        IProductRepository productRepository, 
        IUniqueIdentifierGenerator identifierGenerator,
        IClock clock
    )
    {
        _productRepository = productRepository;
        _identifierGenerator = identifierGenerator;
        _clock = clock;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken ct)
    {
        var product = Product.FromPrimitives
        (
            _identifierGenerator.Generate(),
            request.Name,
            request.Pvp,
            request.CategoryId,
            _clock.GetCurrentInstant()
        );

        await _productRepository.CreateProduct(product, ct);

        return new CreateProductCommandResponse
        {
            ProductData = new CreateProductCommandResponse.Product
            {
                Id = product.Id.Value,
                Name = product.Name.Value,
                Pvp = product.Pvp.Value,
                CategoryId = product.CategoryId?.Value,
                CreatedAt = product.CreateDate
            }
        };
    }
}
