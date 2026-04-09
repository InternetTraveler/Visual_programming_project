using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Exeption;

namespace VisualProgramming.ValueObject.Validais;

public class DescriptionValidator : IValidator<string>
{
    public static int MaxLenghts => 50;
    public static int MinLenghts => 5;
    public void Validate(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new DescriptionNullOrEmptyException(value);

        value = value.Trim();
        if (value.Length > MaxLenghts)
            throw new DescriprionTooLongException(value, value.Length, MaxLenghts);

        if (value.Length < MinLenghts)
            throw new DescriptionTooShortException(value, value.Length, MinLenghts);
    }
}
