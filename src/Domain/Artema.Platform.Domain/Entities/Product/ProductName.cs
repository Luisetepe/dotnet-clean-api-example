using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;

namespace Artema.Platform.Domain.Entities;

public class ProductName : ValueObject<string, ProductName>
{
    protected override void ValidateInput(string input)
    {
        if (string.IsNullOrEmpty(input) || input.Length > 100)
        {
            throw new DomainException($"Value '{input} is not a valid Product Name'");
        }
    }
}