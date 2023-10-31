using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Domain.ValueObjects;

namespace Artema.Platform.Domain.Criteria;

public enum FilterOperatorEnum
{
    EQ,
    NEQ,
    GT,
    GTE,
    LT,
    LTE,
    INC
}

public class FilterOperator : ValueObject<string, FilterOperatorEnum, FilterOperator>
{
    protected override FilterOperatorEnum Transform(string? input)
    {
        return input?.ToLower() switch
        {
            "eq" => FilterOperatorEnum.EQ,
            "neq" => FilterOperatorEnum.NEQ,
            "gt" => FilterOperatorEnum.GT,
            "gte" => FilterOperatorEnum.GTE,
            "lt" => FilterOperatorEnum.LT,
            "lte" => FilterOperatorEnum.LTE,
            "inc" => FilterOperatorEnum.INC,
            _ => throw new InvalidCriteriaException($"The input '{input}' was not a valid Filter operator.")
        };
    }
}