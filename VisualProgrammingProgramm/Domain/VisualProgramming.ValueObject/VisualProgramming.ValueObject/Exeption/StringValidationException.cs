namespace VisualProgramming.ValueObject.Exeption;

public abstract class StringValidationException : Exception
{
    public string Name { get; }
    public int ActualLength { get; }

    protected StringValidationException(string message, string name = "", int actualLength = 0)
        : base(message)
    {
        Name = name;
        ActualLength = actualLength;
    }
}