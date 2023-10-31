using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;

namespace Artema.Platform.Domain.Criteria;

public class Limit : ValueObject<int, Limit>
{
    protected override void ValidateInput(int input)
    {
        if (input <= 0)
        {
            throw new InvalidCriteriaException($"The input '{input}' was not a valid query limit.");
        }
    }
}