using Artema.Platform.Api.Models;

namespace Artema.Platform.Api.Endpoints.SearchProducts;

public record SearchProductsRequest : BaseSearchRequest;

public class SearchProductsRequestValidator : BaseSearchRequestValidator<SearchProductsRequest> { }