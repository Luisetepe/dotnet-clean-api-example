using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;

namespace Artema.Platform.Domain.Entities;

public class CategoryName : ValueObject<string, CategoryName>
{
    protected override void ValidateInput(string input)
    {
        if (string.IsNullOrEmpty(input) || input.Length > 100)
        {
            throw new DomainException($"Value '{input} is not a valid Category Name'");
        }
    }
}
