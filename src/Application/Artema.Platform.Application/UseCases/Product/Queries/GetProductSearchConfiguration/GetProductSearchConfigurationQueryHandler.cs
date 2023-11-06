using Artema.Platform.Application.Interfaces;
using MediatR;

namespace Artema.Platform.Application.UseCases.Queries.GetProductSearchConfiguration;

public class GetProductSearchConfigurationQueryHandler
    : IRequestHandler<GetProductSearchConfigurationQuery, GetProductSearchConfigurationQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductSearchConfigurationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<GetProductSearchConfigurationQueryResponse> Handle
    (
        GetProductSearchConfigurationQuery request,
        CancellationToken cancellationToken
    )
    {
        var configuration = _unitOfWork.ProductRepository.GetSearchConfiguration();

        return Task.FromResult
        (
            new GetProductSearchConfigurationQueryResponse
            {
                FilterFields = configuration.FilterFields,
                OrderByFields = configuration.OrderByFields
            }
        );
    }
}