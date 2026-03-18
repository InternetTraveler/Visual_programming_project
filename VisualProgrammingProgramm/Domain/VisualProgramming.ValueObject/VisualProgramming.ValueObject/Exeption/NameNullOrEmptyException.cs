namespace VisualProgramming.ValueObject.Exeption;

public class NameNullOrEmptyException : NameValidationException
{
    private const string DefaultMessage = "Название блока не может быть null или пустым";

    public NameNullOrEmptyException(string blockName)
        : base(DefaultMessage, blockName) { }

    public NameNullOrEmptyException(string blockName, int lenght)
        : base(DefaultMessage, blockName, lenght) { }
}