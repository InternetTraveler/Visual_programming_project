namespace VisualProgramming.Domain.Exceptions;

public class PortContainmentException : ContainmentException
{
    public PortContainmentException(object container, object item) 
        : base(container, item){}
}
