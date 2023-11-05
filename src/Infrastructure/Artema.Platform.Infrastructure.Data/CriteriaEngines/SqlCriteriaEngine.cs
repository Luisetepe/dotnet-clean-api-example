using System.Linq.Expressions;
using System.Text;
using Artema.Platform.Domain.Criteria;
using Artema.Platform.Infrastructure.Data.Exceptions;
using Dapper;

namespace Artema.Platform.Infrastructure.Data.CriteriaEngines;

public static class SqlCriteriaEngine
{
    private static Dictionary<FilterOperatorEnum, string> OperatorMap = new()
    {
        { FilterOperatorEnum.EQ, "=" },
        { FilterOperatorEnum.NEQ, "<>" },
        { FilterOperatorEnum.GT, ">" },
        { FilterOperatorEnum.GTE, ">=" },
        { FilterOperatorEnum.LT, "<" },
        { FilterOperatorEnum.LTE, "<=" }
    };

    public static (string, DynamicParameters) ApplyCriteria<T>(string initialSelectQuery, string tableAlias, SearchCriteria criteria)
    {
        var queryBuilder = new StringBuilder(initialSelectQuery);
        var paramBuilder = new DynamicParameters();

        if(criteria.HasFilters())
        {
            queryBuilder.Append(" WHERE ");
            queryBuilder.Append(string.Join(" AND ", criteria.Filters!.Select(filter =>
            {
                var searchName = filter.FilterField.Value.ToLower();
                if (typeof(T).GetProperties().All(prop => prop.Name.ToLower() != searchName))
                {
                    throw new InvalidColumnException(typeof(T).Name, filter.FilterField.Value);
                }

                var param = Expression.Parameter(typeof(T), "filterParam");
                var propExpression = Expression.Property(param, searchName);

                var value = filter.FilterValue;
                if (propExpression.Type != typeof(string))
                    value = Convert.ChangeType(value, propExpression.Type);

                paramBuilder.Add($"@{searchName}", value);

                return filter.FilterOperator.Value switch
                {
                    FilterOperatorEnum.EQ or FilterOperatorEnum.NEQ or FilterOperatorEnum.GT
                    or FilterOperatorEnum.GTE or FilterOperatorEnum.LT or FilterOperatorEnum.LTE
                        => $"{tableAlias}.{searchName} {OperatorMap[filter.FilterOperator.Value]} @{searchName}",
                    FilterOperatorEnum.INC => $"{tableAlias}.{searchName} ILIKE '%' || @{searchName} || '%'",
                };
            })));
        }

        if(criteria.HasOrder())
        {
            var searchName = criteria.Order!.OrderBy.Value.ToLower();
            if (typeof(T).GetProperties().All(prop => prop.Name.ToLower() != searchName))
            {
                throw new InvalidColumnException(typeof(T).Name, criteria.Order!.OrderBy.Value);
            }

            queryBuilder.Append(" ORDER BY ");
            queryBuilder.Append($"{tableAlias}.{searchName}");
            queryBuilder.Append(criteria.Order!.OrderType.Value == OrderTypeEnum.DESC ? " DESC" : " ASC");
        }

        if(criteria.HasLimit())
        {
            queryBuilder.Append(" LIMIT ");
            queryBuilder.Append(criteria.Limit!.Value);
        }

        if(criteria.HasOffset())
        {
            queryBuilder.Append(" OFFSET ");
            queryBuilder.Append(criteria.Offset!.Value);
        }

        return (queryBuilder.ToString(), paramBuilder);
    }
}