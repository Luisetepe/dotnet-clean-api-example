using Artema.Platform.Application.Interfaces;
using Artema.Platform.Domain.Entities;
using MediatR;
using NodaTime;

namespace Artema.Platform.Application.UseCases.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUniqueIdentifierGenerator _identifierGenerator;
    private readonly IClock _clock;

    public CreateProductCommandHandler
    (
        IUnitOfWork unitOfWork,
        IUniqueIdentifierGenerator identifierGenerator,
        IClock clock
    )
    {
        _unitOfWork = unitOfWork;
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

        await _unitOfWork.ProductRepository.CreateProduct(product, ct);
        await _unitOfWork.SaveAsync(ct);

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
