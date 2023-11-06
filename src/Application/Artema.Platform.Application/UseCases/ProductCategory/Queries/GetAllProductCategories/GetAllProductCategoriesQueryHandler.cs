using Artema.Platform.Application.Interfaces;
using MediatR;

namespace Artema.Platform.Application.UseCases.Queries.GetAllProductCategories;

public class GetAllProductCategoriesQueryHandler : IRequestHandler<GetAllProductCategoriesQuery, GetAllProductCategoriesQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProductCategoriesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllProductCategoriesQueryResponse> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var productCategories = await _unitOfWork.ProductCategoryRepository.GetAllProductCategories(cancellationToken);

        return new GetAllProductCategoriesQueryResponse
        {
            ProductCategories = productCategories.Select(pc => new GetAllProductCategoriesQueryResponse.ProductCategory
            {
                Id = pc.Id.Value,
                Name = pc.Name.Value,
                IsService = pc.IsService
            })
        };
    }
}