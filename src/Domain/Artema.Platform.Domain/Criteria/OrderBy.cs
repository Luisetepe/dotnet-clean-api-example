using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;

namespace Artema.Platform.Domain.Criteria;

public class OrderBy : ValueObject<string, OrderBy>
{
    protected override void ValidateInput(string? input)
    {
        if (string.IsNullOrEmpty(input?.Trim()))
        {
            throw new InvalidCriteriaException($"The input '{input}' was not a valid OrderBy field.");
        }
    }
}