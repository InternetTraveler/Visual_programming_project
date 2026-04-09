namespace VisualProgramming.Domain.Exceptions;

public class GrafContainmentException : ContainmentException
{
    public GrafContainmentException(object container, object item) : 
        base(container, item) {}
}
