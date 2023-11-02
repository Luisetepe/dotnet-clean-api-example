using Artema.Platform.Application.Interfaces;
using Artema.Platform.Domain.Entities;
using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;
using MediatR;

namespace Artema.Platform.Application.UseCases.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetProductById(EntityId.FromValue(request.Id), cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product), nameof(Product.Id), request.Id.ToString());

        product.UpdateData(
            request.Name,
            request.Pvp,
            request.CategoryId
        );

        await _unitOfWork.ProductRepository.UpdateProduct(product, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}
