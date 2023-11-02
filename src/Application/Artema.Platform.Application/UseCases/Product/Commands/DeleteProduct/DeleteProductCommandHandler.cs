using Artema.Platform.Application.Interfaces;
using Artema.Platform.Domain.Entities;
using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;
using MediatR;

namespace Artema.Platform.Application.UseCases.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken ct)
    {
        var productId = EntityId.FromValue(request.Id);
        var productExists = await _unitOfWork.ProductRepository.ExistsProduct(productId, ct);

        if (!productExists)
        {
            throw new EntityNotFoundException(nameof(Product), nameof(Product.Id), request.Id.ToString());
        }

        await _unitOfWork.ProductRepository.DeleteProduct(productId, ct);
        await _unitOfWork.SaveAsync(ct);
    }
}
