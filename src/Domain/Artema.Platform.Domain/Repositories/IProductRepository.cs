using Artema.Platform.Domain.Criteria;
using Artema.Platform.Domain.Entities;
using Artema.Platform.Domain.ValueObjects;

namespace Artema.Platform.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> SearchProducts(SearchCriteria criteria, CancellationToken ct = default);
    Task<IEnumerable<Product>> GetAllProducts(CancellationToken ct = default);
    Task<Product?> GetProductById(EntityId id, CancellationToken ct = default);
    Task<Product> CreateProduct(Product product, CancellationToken ct = default);
    Task UpdateProduct(Product product, CancellationToken ct = default);
    Task DeleteProduct(EntityId id, CancellationToken ct = default);
    Task<bool> ExistsProduct(EntityId id, CancellationToken ct = default);
}
