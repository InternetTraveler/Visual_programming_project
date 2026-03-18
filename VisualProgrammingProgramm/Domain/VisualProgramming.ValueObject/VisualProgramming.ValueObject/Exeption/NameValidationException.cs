namespace VisualProgramming.ValueObject.Exeption;

public abstract class NameValidationException : Exception
{
    public string BlockName { get; }
    public int ActualLength { get; }

    protected NameValidationException(string message, string blockName = "", int actualLength = 0)
        : base(message)
    {
        BlockName = blockName;
        ActualLength = actualLength;
    }
}