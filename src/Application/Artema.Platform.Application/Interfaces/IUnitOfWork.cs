using Artema.Platform.Domain.Repositories;

namespace Artema.Platform.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    IProductCategoryRepository ProductCategoryRepository { get; }
    Task SaveAsync(CancellationToken ct = default);
}
