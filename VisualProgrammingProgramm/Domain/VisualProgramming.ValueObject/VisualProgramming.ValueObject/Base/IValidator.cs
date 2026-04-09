namespace VisualProgramming.ValueObject.Base;

public interface IValidator<T> where T: IEquatable<T>
{
    void Validate(T value);
}