namespace VisualProgramming.Domain.Exceptions;

public class NodeContainmentException : ContainmentException
{
    public NodeContainmentException(object container, object item) 
        : base(container, item){}
}
