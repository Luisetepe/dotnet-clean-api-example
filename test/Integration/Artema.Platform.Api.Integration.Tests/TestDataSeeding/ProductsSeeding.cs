using Artema.Platform.Infrastructure.Data.DbContexts;
using Artema.Platform.Infrastructure.Data.Seeding;
using Artema.Platform.Infrastructure.Data.Services;
using Artema.Platform.Infrastructure.Data.TableModels;
using Microsoft.EntityFrameworkCore;

namespace Artema.Platform.Api.Integration.Tests.TestDataSeeding;

public class ProductsSeeding : IDatabaseSeeder
{
    public async Task SeedTestDataAsync(ArtemaPlatformDbContext dbContext)
    {
        var generator = new PgUniqueIdentifierGenerator();

        var categories = new List<ProductCategoryTableModel>
        {
            new() { Id = generator.Generate(), Name = "Bebidas", IsService = false },
            new() { Id = generator.Generate(), Name = "Snacks", IsService = false },
            new() { Id = generator.Generate(), Name = "Streaming", IsService = true },

        };

        var products = new List<ProductTableModel>
        {
            new() { Id = generator.Generate(), Name = "CocaCola", Pvp = 2500, CategoryId = categories[0].Id },
            new() { Id = generator.Generate(), Name = "Fanta Naranja", Pvp = 1500, CategoryId = categories[0].Id },
            new() { Id = generator.Generate(), Name = "Fanta Limon", Pvp = 1500, CategoryId = categories[0].Id },
            new() { Id = generator.Generate(), Name = "Aquarius", Pvp = 3000, CategoryId = categories[0].Id },
            new() { Id = generator.Generate(), Name = "Monster", Pvp = 3500, CategoryId = categories[0].Id },
            new() { Id = generator.Generate(), Name = "Doritos", Pvp = 1200, CategoryId = categories[1].Id },
            new() { Id = generator.Generate(), Name = "Pelotazos", Pvp = 1700, CategoryId = categories[1].Id },
            new() { Id = generator.Generate(), Name = "Fantasmitas", Pvp = 1350, CategoryId = categories[1].Id },
            new() { Id = generator.Generate(), Name = "Fritos", Pvp = 1350, CategoryId = categories[1].Id },
            new() { Id = generator.Generate(), Name = "Gublins", Pvp = 1350, CategoryId = categories[1].Id },
            new() { Id = generator.Generate(), Name = "Netflix", Pvp = 4500, CategoryId = categories[2].Id },
            new() { Id = generator.Generate(), Name = "HBO Max", Pvp = 4500, CategoryId = categories[2].Id },
            new() { Id = generator.Generate(), Name = "Amazon Prime", Pvp = 4500, CategoryId = categories[2].Id },
        };

        if(!await dbContext.ProductCategories.AnyAsync())
            await dbContext.ProductCategories.AddRangeAsync(categories);
        if(!await dbContext.Products.AnyAsync())
            await dbContext.Products.AddRangeAsync(products);
    }
}
