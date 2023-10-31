using System.Linq.Expressions;
using System.Reflection;

namespace Artema.Platform.Domain.ValueObjects;

public abstract class ValueObject<TValue, TThis> 
    where TThis : ValueObject<TValue, TThis>, new()
{
    private static readonly Func<TThis> Factory = (Func<TThis>) Expression.Lambda(typeof (Func<TThis>), Expression.New(typeof (TThis).GetTypeInfo().DeclaredConstructors.First(), Array.Empty<Expression>())).Compile();

    protected virtual void ValidateInput(TValue input) {}

    public TValue Value { get; protected set; } = default!;

    public static TThis FromValue(TValue item)
    {
        TThis @this = Factory();
        @this.ValidateInput(item);
        @this.Value = item;
        return @this;
    }

    protected virtual bool Equals(ValueObject<TValue, TThis> other) => EqualityComparer<TValue>.Default.Equals(Value, other.Value);

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        return obj.GetType() == GetType() && Equals((ValueObject<TValue, TThis>) obj);
    }

    public override int GetHashCode() => Value is null ? 0 : EqualityComparer<TValue>.Default.GetHashCode(Value);

    public static bool operator ==(ValueObject<TValue, TThis> a, ValueObject<TValue, TThis> b)
    {
        if ((object?) a == null && (object?) b == null)
            return true;
        return (object?) a != null && (object?) b != null && a.Equals(b);
    }

    public static bool operator !=(ValueObject<TValue, TThis> a, ValueObject<TValue, TThis> b) => !(a == b);

    public override string ToString() => Value?.ToString() ?? "";
}

public abstract class ValueObject<TInput, TValue, TThis> 
    where TThis : ValueObject<TInput, TValue, TThis>, new()
{
    private static readonly Func<TThis> Factory = (Func<TThis>) Expression.Lambda(typeof (Func<TThis>), Expression.New(typeof (TThis).GetTypeInfo().DeclaredConstructors.First(), Array.Empty<Expression>())).Compile();

    protected abstract TValue Transform(TInput input);

    public TValue Value { get; protected set; } = default!;

    public static TThis FromValue(TInput item)
    {
        TThis @this = Factory();
        @this.Value = @this.Transform(item);
        return @this;
    }

    protected virtual bool Equals(ValueObject<TInput, TValue, TThis> other) => EqualityComparer<TValue>.Default.Equals(Value, other.Value);

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        return obj.GetType() == GetType() && Equals((ValueObject<TInput, TValue, TThis>) obj);
    }

    public override int GetHashCode() => Value is null ? 0 : EqualityComparer<TValue>.Default.GetHashCode(Value);

    public static bool operator ==(ValueObject<TInput, TValue, TThis> a, ValueObject<TInput, TValue, TThis> b)
    {
        if ((object?) a == null && (object?) b == null)
            return true;
        return (object?) a != null && (object?) b != null && a.Equals(b);
    }

    public static bool operator !=(ValueObject<TInput, TValue, TThis> a, ValueObject<TInput, TValue, TThis> b) => !(a == b);

    public override string? ToString() => Value?.ToString();
}