using Artema.Platform.Domain.Criteria;
using Artema.Platform.Domain.Entities;

namespace Artema.Platform.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> SearchProducts(SearchCriteria criteria, CancellationToken ct = default);
    Task<IEnumerable<Product>> GetAllProducts(CancellationToken ct = default);
    Task<Product?> GetProductById(Guid id, CancellationToken ct = default);
    Task<Product> CreateProduct(Product product, CancellationToken ct = default);
}