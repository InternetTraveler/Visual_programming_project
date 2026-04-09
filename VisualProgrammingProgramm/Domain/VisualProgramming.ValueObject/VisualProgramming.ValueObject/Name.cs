using VisualProgramming.ValueObject.Base;

namespace VisualProgramming.ValueObject;

public class Name : ValueObjects<string>
{
    private static NameValidator validator = new NameValidator();

    public Name(string name) : base(validator, name) { }

    public static implicit operator Name(string value) => new(value);
}