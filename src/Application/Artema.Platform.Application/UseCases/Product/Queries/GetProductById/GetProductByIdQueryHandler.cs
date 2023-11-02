using Artema.Platform.Application.Interfaces;
using Artema.Platform.Domain.Entities;
using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;
using MediatR;

namespace Artema.Platform.Application.UseCases.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetProductById(EntityId.FromValue(request.Id), cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product), nameof(Product.Id), request.Id.ToString());

        return new GetProductByIdQueryResponse
        {
            ProductData = new GetProductByIdQueryResponse.Product
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
