using Artema.Platform.Domain.Repositories;
using MediatR;

namespace Artema.Platform.Application.UseCases.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository
            .GetProductById(request.Id, cancellationToken);

        return new GetProductByIdQueryResponse
        {
            ProductData = product is null ? null : new GetProductByIdQueryResponse.Product
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