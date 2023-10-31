using Artema.Platform.Domain.Exceptions;

namespace Artema.Platform.Domain.ValueObjects;

public class EntityId : ValueObject<Guid, EntityId>
{
    protected override void ValidateInput(Guid input)
    {
        if (input == Guid.Empty)
        {
            throw new DomainException($"Value '{input} is not a valid unique identifier'");
        }
    }
}