using Artema.Platform.Domain.Entities.ProductCategory;

namespace Artema.Platform.Domain.Repositories;

public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategory>> GetAllProductCategories(CancellationToken ct = default);
}
