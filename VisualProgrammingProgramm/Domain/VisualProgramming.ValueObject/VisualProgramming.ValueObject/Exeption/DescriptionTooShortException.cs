namespace VisualProgramming.ValueObject.Exeption;

internal class DescriptionTooShortException : StringValidationException
{
    public int MinAllowedLength { get; }

    public DescriptionTooShortException(string name, int actualLength, int minAllowedLength)
        : base(FormatMessage(name, actualLength, minAllowedLength), name, actualLength)
    {
        MinAllowedLength = minAllowedLength;
    }

    private static string FormatMessage(string name, int actualLength, int minAllowedLength)
    {
        return $"Длина описания '{name}' ({actualLength} символов) превышает " +
            $"максимально допустимую длину в {minAllowedLength} символов";
    }
}