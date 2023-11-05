namespace Artema.Platform.Domain.Criteria;

public class SearchCriteria
{
    public Filter[]? Filters { get; }
    public Order? Order { get; }
    public Limit? Limit { get; }
    public Offset? Offset { get; }

    public SearchCriteria(Filter[]? filters = null, Order? order = null, Limit? limit = null, Offset? offset = null)
    {
        Filters = filters;
        Order = order;
        Limit = limit;
        Offset = offset;
    }

    public bool HasFilters()
    {
        return Filters is not null && Filters.Any();
    }

    public bool HasOrder()
    {
        return Order is not null;
    }

    public bool HasLimit()
    {
        return Limit is not null;
    }

    public bool HasOffset()
    {
        return Offset is not null;
    }
}