using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;

namespace Artema.Platform.Domain.Entities;

public class ProductPvp : ValueObject<long, ProductPvp>
{
    protected override void ValidateInput(long input)
    {
        if (input < 0)
        {
            throw new DomainException($"Value '{input} is not a valid Product Pvp'");
        }
    }
}