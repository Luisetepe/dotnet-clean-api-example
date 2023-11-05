using System.Linq.Expressions;
using System.Reflection;
using Artema.Platform.Domain.Criteria;
using Artema.Platform.Infrastructure.Data.Exceptions;

namespace Artema.Platform.Infrastructure.Data.CriteriaEngines;

public static class EntityFrameworkCriteriaEngine
{
    private static readonly MethodInfo StringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
    private static readonly MethodInfo StringToLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;

    public static IQueryable<T> ApplyCriteria<T>(IQueryable<T> query, SearchCriteria criteria)
    {
        if (criteria.HasFilters())
        {
            query = ApplyFilters(query, criteria.Filters!);
        }

        if (criteria.HasOrder())
        {
            query = ApplyOrder(query, criteria.Order!);
        }

        if (criteria.HasLimit())
        {
            query = query.Take(criteria.Limit!.Value);
        }

        if (criteria.HasOffset())
        {
            query = query.Skip(criteria.Offset!.Value);
        }

        return query;
    }

    private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> query, Order order)
    {
        var searchName = order.OrderBy.Value.Replace("-", string.Empty).ToLower();
        if (typeof(T).GetProperties().All(prop => prop.Name.ToLower() != searchName))
        {
            throw new InvalidColumnException(typeof(T).Name, order.OrderBy.Value);
        }

        var param = Expression.Parameter(typeof(T), "orderParam");
        var propExpression = Expression.Property(param, order.OrderBy.Value);
        var expr = Expression.Lambda<Func<T, object>>(Expression.Convert(propExpression, typeof(object)), param);

        return order.OrderType.Value switch
        {
            OrderTypeEnum.ASC => query.OrderBy(expr),
            OrderTypeEnum.DESC => query.OrderByDescending(expr),
        };
    }

    private static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, IEnumerable<Filter> filters)
    {
        foreach (var filter in filters)
        {
            var searchName = filter.FilterField.Value.Replace("-", string.Empty).ToLower();
            if (typeof(T).GetProperties().All(prop => prop.Name.ToLower() != searchName))
            {
                throw new InvalidColumnException(typeof(T).Name, filter.FilterField.Value);
            }

            var param = Expression.Parameter(typeof(T), "filterParam");
            var propExpression = Expression.Property(param, filter.FilterField.Value);

            var value = filter.FilterValue;
            if (propExpression.Type != typeof(string))
                value = Convert.ChangeType(value, propExpression.Type);

            var filterLambda = Expression.Lambda<Func<T, bool>>(
                filter.FilterOperator.Value switch
                {
                    FilterOperatorEnum.EQ => Expression.Equal(
                        propExpression,
                        Expression.Constant(value)
                    ),
                    FilterOperatorEnum.NEQ => Expression.NotEqual(
                        propExpression,
                        Expression.Constant(value)
                    ),
                    FilterOperatorEnum.GT => Expression.GreaterThan(
                        propExpression,
                        Expression.Constant(value)
                    ),
                    FilterOperatorEnum.GTE => Expression.GreaterThanOrEqual(
                        propExpression,
                        Expression.Constant(value)
                    ),
                    FilterOperatorEnum.LT => Expression.LessThan(
                        propExpression,
                        Expression.Constant(value)
                    ),
                    FilterOperatorEnum.LTE => Expression.LessThanOrEqual(
                        propExpression,
                        Expression.Constant(value)
                    ),
                    FilterOperatorEnum.INC => Expression.Call(
                        Expression.Call(
                            propExpression,
                            StringToLowerMethod
                        ),
                        StringContainsMethod,
                        Expression.Constant(value.ToString()?.ToLower())
                    )
                },
                param
            );

            query = query.Where(filterLambda);
        }

        return query;
    }
}