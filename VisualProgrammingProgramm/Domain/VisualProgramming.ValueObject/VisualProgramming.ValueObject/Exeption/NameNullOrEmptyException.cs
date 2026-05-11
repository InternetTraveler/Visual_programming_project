namespace VisualProgramming.ValueObject.Exeption;

public class NameNullOrEmptyException : StringValidationException
{
    private const string DefaultMessage = "Название не может быть null или пустым";

    public NameNullOrEmptyException(string blockName)
        : base(DefaultMessage, blockName) { }

    public NameNullOrEmptyException(string blockName, int lenght)
        : base(DefaultMessage, blockName, lenght) { }
}