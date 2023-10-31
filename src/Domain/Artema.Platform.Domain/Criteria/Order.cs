namespace Artema.Platform.Domain.Criteria;

public class Order
{
    public OrderBy OrderBy { get; }
    public OrderType OrderType { get; }

    private Order(OrderBy orderBy, OrderType orderType)
    {
        OrderBy = orderBy;
        OrderType = orderType;
    }

    public static Order FromPrimitives(string field, string type)
    {
        return new Order(OrderBy.FromValue(field), OrderType.FromValue(type));
    }
}