using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;

namespace Artema.Platform.Domain.Criteria;

public enum OrderTypeEnum
{
    ASC,
    DESC
}

public class OrderType : ValueObject<string, OrderTypeEnum, OrderType>
{
    protected override OrderTypeEnum Transform(string? input)
    {
        return input?.ToLower() switch
        {
            "asc" => OrderTypeEnum.ASC,
            "desc" => OrderTypeEnum.DESC,
            _ => throw new InvalidCriteriaException($"The input '{input}' was not a valid OrderType operator.")
        };
    }
}