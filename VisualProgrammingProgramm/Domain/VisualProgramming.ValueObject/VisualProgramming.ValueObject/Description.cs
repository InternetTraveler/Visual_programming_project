using VisualProgramming.ValueObject.Base;
using VisualProgramming.ValueObject.Validais;

namespace VisualProgramming.ValueObject;

public class Description : ValueObjects<string>
{
    private static DescriptionValidator validator = new DescriptionValidator();

    public Description(string name) : base(validator, name) { }

    public static implicit operator Description(string value) => new(value);
}