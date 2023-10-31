using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;

namespace Artema.Platform.Domain.Criteria;

public class Offset : ValueObject<int, Offset>
{
    protected override void ValidateInput(int input)
    {
        if (input < 0)
        {
            throw new InvalidCriteriaException($"The input '{input}' was not a valid query offset.");
        }
    }
}