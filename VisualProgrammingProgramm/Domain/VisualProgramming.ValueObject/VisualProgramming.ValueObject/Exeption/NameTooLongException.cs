namespace VisualProgramming.ValueObject.Exeption;

public class NameTooLongException : StringValidationException
{
    public int MaxAllowedLength { get; }

    public NameTooLongException(string name, int actualLength, int maxAllowedLength)
        : base(FormatMessage(name, actualLength, maxAllowedLength), name, actualLength)
    {
        MaxAllowedLength = maxAllowedLength;
    }

    private static string FormatMessage(string name, int actualLength, int maxAllowedLength)
    {
        return $"Длина имени '{name}' ({actualLength} символов) превышает " +
            $"максимально допустимую длину в {maxAllowedLength} символов";
    }
}