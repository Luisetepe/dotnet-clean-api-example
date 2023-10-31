using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;

namespace Artema.Platform.Domain.Criteria;

public class FilterField : ValueObject<string, FilterField>
{
    protected override void ValidateInput(string? input)
    {
        if (string.IsNullOrEmpty(input?.Trim()))
        {
            throw new InvalidCriteriaException($"The input '{input}' was not a valid Filter field.");
        }
    }
}