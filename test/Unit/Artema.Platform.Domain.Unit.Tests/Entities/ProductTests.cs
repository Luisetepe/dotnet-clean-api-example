using Artema.Platform.Domain.Entities;
using Artema.Platform.Domain.Exceptions;
using NodaTime;

namespace Artema.Platform.Domain.Unit.Tests.Entities;

public class ProductTests
{
    [Theory]
    [InlineData("a4ccab72-aba3-43c9-aaeb-a546242bf6ef", "CocaCola", 200, "00000000-0000-0000-0000-000000000000")]
    [InlineData("00000000-0000-0000-0000-000000000000", "CocaCola", 200, null)]
    [InlineData("a4ccab72-aba3-43c9-aaeb-a546242bf6ef", "", 200, "555d9347-57be-44aa-b462-13d394277987")]
    [InlineData("a4ccab72-aba3-43c9-aaeb-a546242bf6ef", null, 200, null)]
    [InlineData("a4ccab72-aba3-43c9-aaeb-a546242bf6ef", "CocaCola", -10, "555d9347-57be-44aa-b462-13d394277987")]
    public void Given_Bad_Inputs_Product_Creation_Should_Fail(string id, string name, long pvp, string? categoryId)
    {
        Should.Throw<DomainException>(() =>
            Product.FromPrimitives(
                Guid.Parse(id), 
                name, 
                pvp, 
                categoryId is null ? null : Guid.Parse(categoryId),
                SystemClock.Instance.GetCurrentInstant()
            )
        );
    }
    
    [Theory]
    [InlineData("a4ccab72-aba3-43c9-aaeb-a546242bf6ef", "CocaCola", 200, "555d9347-57be-44aa-b462-13d394277987")]
    [InlineData("a4ccab72-aba3-43c9-aaeb-a546242bf6ef", "Fanta_Naranja", 200, "555d9347-57be-44aa-b462-13d394277987")]
    [InlineData("a4ccab72-aba3-43c9-aaeb-a546242bf6ef", "Amazon Prime", 200, "555d9347-57be-44aa-b462-13d394277987")]
    [InlineData("a4ccab72-aba3-43c9-aaeb-a546242bf6ef", "CocaCola", 0, "555d9347-57be-44aa-b462-13d394277987")]
    public void Given_Valid_Inputs_Product_Creation_Should_Succeed(string id, string name, long pvp, string? categoryId)
    {
        var now = SystemClock.Instance.GetCurrentInstant();
        var product = Product.FromPrimitives(
            Guid.Parse(id),
            name,
            pvp,
            categoryId is null ? null : Guid.Parse(categoryId),
            now
        );
        
        product.Id.Value.ToString().ShouldBe(id);
        product.Name.Value.ShouldBe(name);
        product.Pvp.Value.ShouldBe(pvp);
        if (categoryId is null)
            product.CategoryId.ShouldBeNull();
        else
            product.CategoryId!.Value.ToString().ShouldBe(categoryId);
        product.CreateDate.ShouldBe(now);
        
    }
}
