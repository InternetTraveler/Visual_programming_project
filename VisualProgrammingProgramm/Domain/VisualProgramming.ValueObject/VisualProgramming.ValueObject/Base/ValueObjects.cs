namespace VisualProgramming.ValueObject.Base;
public abstract class ValueObjects<T> : IEquatable<ValueObjects<T>> where T : IEquatable<T>
{
    public T Value { get; }

    protected ValueObjects(IValidator<T> validator, T value)
    {
        if (validator == null)
            throw new ArgumentNullException("AAAAAAAAAAAAAAAAA");

        validator.Validate(value);
        Value = value;
    }

    public override string ToString() => Value!.ToString() ?? GetType().ToString();

    public override int GetHashCode() => Value!.GetHashCode();

    public override bool Equals(object? obj) => Equals(obj as ValueObjects<T>);

    public bool Equals(ValueObjects<T>? other)
    {
        if (other is null)
            throw new ArgumentNullException("other!");

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return other.Value!.Equals(Value);
    }

    public static bool operator ==(ValueObjects<T>? left, ValueObjects<T>? right)
        => Equals(left, right);

    public static bool operator !=(ValueObjects<T>? left, ValueObjects<T>? right)
        => !(left == right);
}