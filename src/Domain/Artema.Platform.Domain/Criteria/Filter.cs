namespace Artema.Platform.Domain.Criteria;

public class Filter
{
    public object FilterValue { get; }
    public FilterField FilterType { get; }
    public FilterOperator FilterOperator { get; }

    public Filter(object filterValue, FilterField filterType, FilterOperator filterOperator)
    {
        FilterValue = filterValue;
        FilterType = filterType;
        FilterOperator = filterOperator;
    }
    
    public static Filter FromPrimitives(object filterValue, string filterField, string filterOperator)
    {
        return new Filter
        (
            filterValue,
            FilterField.FromValue(filterField),
            FilterOperator.FromValue(filterOperator)
        );
    }
}