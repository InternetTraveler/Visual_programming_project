namespace VisualProgramming.ValueObject.Exeption;

internal class NameTooShortException : StringValidationException
{
    public int MinAllowedLength { get; }

    public NameTooShortException(string name, int actualLength, int minAllowedLength)
        : base(FormatMessage(name, actualLength, minAllowedLength), name, actualLength)
    {
        MinAllowedLength = minAllowedLength;
    }

    private static string FormatMessage(string name, int actualLength, int minAllowedLength)
    {
        return $"Длина названия '{name}' ({actualLength} символов) превышает " +
            $"максимально допустимую длину в {minAllowedLength} символов";
    }
}