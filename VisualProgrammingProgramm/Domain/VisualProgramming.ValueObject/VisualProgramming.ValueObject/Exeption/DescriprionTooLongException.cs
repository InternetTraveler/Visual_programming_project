namespace VisualProgramming.ValueObject.Exeption;

public class DescriprionTooLongException : StringValidationException
{
    public int MaxAllowedLength { get; }

    public DescriprionTooLongException(string name, int actualLength, int maxAllowedLength)
        : base(FormatMessage(name, actualLength, maxAllowedLength), name, actualLength)
    {
        MaxAllowedLength = maxAllowedLength;
    }

    private static string FormatMessage(string name, int actualLength, int maxAllowedLength)
    {
        return $"Длина описания '{name}' ({actualLength} символов) превышает " +
            $"максимально допустимую длину в {maxAllowedLength} символов";
    }
}