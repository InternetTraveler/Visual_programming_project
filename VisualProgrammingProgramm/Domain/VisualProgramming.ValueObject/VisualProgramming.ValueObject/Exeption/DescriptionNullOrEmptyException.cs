namespace VisualProgramming.ValueObject.Exeption;

public class DescriptionNullOrEmptyException : StringValidationException
{
    private const string DefaultMessage = "Описание не может быть null или пустым";

    public DescriptionNullOrEmptyException(string discription)
        : base(DefaultMessage, discription) { }

    public DescriptionNullOrEmptyException(string discription, int lenght)
        : base(DefaultMessage, discription, lenght) { }
}