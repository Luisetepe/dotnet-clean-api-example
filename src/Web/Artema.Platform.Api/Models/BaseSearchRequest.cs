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

public abstract record BaseSearchRequest
{
    public SearchFilter[]? Filters { get; init; }
    public string? OrderBy { get; init; }
    public string? OrderType { get; init; }
    public int? Limit { get; init; }
    public int? Offset { get; init; }

    public virtual SearchCriteria ToCriteria()
    {
        return new SearchCriteria
        (
            Filters?.Select(f => Filter.FromPrimitives(f.Value, f.Field, f.Operator)).ToArray(),
            OrderBy is not null && OrderType is not null ? Order.FromPrimitives(OrderBy, OrderType) : null,
            Limit.HasValue ? Domain.Criteria.Limit.FromValue(Limit.Value) : null,
            Offset.HasValue ? Domain.Criteria.Offset.FromValue(Offset.Value) : null
        );
    }
}

public abstract class BaseSearchRequestValidator<T> : Validator<T> where T : BaseSearchRequest
{
    protected BaseSearchRequestValidator()
    {
        When(x => x.OrderBy is not null, () =>
        {
            RuleFor(x => x.OrderBy).NotEmpty();
        });
        When(x => x.OrderType is not null, () =>
        {
            RuleFor(x => x.OrderType).NotEmpty();
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
