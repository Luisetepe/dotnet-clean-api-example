using Artema.Platform.Domain.Criteria;
using FastEndpoints;
using FluentValidation;
using Order = Artema.Platform.Domain.Criteria.Order;

namespace Artema.Platform.Api.Models;

public record SearchFilter
{
    public string Field { get; init; } = default!;
    public string Value { get; init; } = default!;
    public string Operator { get; init; } = default!;
}

public record SearchOrder
{
    public string OrderBy { get; init; } = default!;
    public string OrderType { get; init; } = default!;

}

public abstract record BaseSearchRequest
{
    public SearchFilter[]? Filters { get; init; }
    public SearchOrder? Order { get; init; }
    public int? Limit { get; init; }
    public int? Offset { get; init; }

    public virtual SearchCriteria ToCriteria()
    {
        return new SearchCriteria
        (
            Filters?.Select(f => Domain.Criteria.Filter.FromPrimitives(f.Value, f.Field, f.Operator)).ToArray(),
            Order is not null ? Domain.Criteria.Order.FromPrimitives(Order.OrderBy, Order.OrderType) : null,
            Limit.HasValue ? Domain.Criteria.Limit.FromValue(Limit.Value) : null,
            Offset.HasValue ? Domain.Criteria.Offset.FromValue(Offset.Value) : null
        );
    }
}

public abstract class BaseSearchRequestValidator<T> : Validator<T> where T : BaseSearchRequest
{
    protected BaseSearchRequestValidator()
    {
        When(x => x.Order is not null, () =>
        {
            RuleFor(x => x.Order!.OrderBy)
                .NotEmpty();

            RuleFor(x => x.Order!.OrderType)
                .NotEmpty()
                .Must(x => x.ToLower() == "asc" || x.ToLower() == "desc");
        });
        When(x => x.Filters is not null, () =>
        {
            RuleForEach(x => x.Filters)
                .ChildRules(filter =>
                {
                    filter.RuleFor(x => x.Field)
                        .NotEmpty()
                        .MaximumLength(100);
                    filter.RuleFor(x => x.Value)
                        .NotEmpty()
                        .MaximumLength(100);
                    filter.RuleFor(x => x.Operator)
                        .NotEmpty();
                });
        });
        When(x => x.Limit is not null, () =>
        {
            RuleFor(x => x.Limit)
                .GreaterThan(0);
        });
        When(x => x.Offset is not null, () =>
        {
            RuleFor(x => x.Offset)
                .GreaterThanOrEqualTo(0);
        });
    }
}
