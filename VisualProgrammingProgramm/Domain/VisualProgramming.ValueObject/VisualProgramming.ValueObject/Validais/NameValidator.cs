using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Exeption;

internal class NameValidator : IValidator<string>
{
    public static int MaxLenghts => 50;
    public static int MinLenghts => 5;
    public void Validate(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new NameNullOrEmptyException(value);

        value = value.Trim();
        if (value.Length > MaxLenghts)
            throw new NameTooLongException(value, value.Length, MaxLenghts);

        if (value.Length < MinLenghts)
            throw new NameTooShortException(value, value.Length, MinLenghts);
    }
}