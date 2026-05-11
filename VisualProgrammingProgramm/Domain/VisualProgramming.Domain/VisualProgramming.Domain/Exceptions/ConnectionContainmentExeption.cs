namespace VisualProgramming.Domain.Exceptions;

public class ConnectionContainmentExeption : ContainmentException
{
    public ConnectionContainmentExeption(object container, object item) 
        : base(container, item) {}
}
