namespace Artema.Platform.Application.UseCases.Queries.GetAllProductCategories;

public record GetAllProductCategoriesQueryResponse
{
    public record ProductCategory
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public bool IsService { get; set; }
    }

    public IEnumerable<ProductCategory> ProductCategories { get; init; } = default!;
}
