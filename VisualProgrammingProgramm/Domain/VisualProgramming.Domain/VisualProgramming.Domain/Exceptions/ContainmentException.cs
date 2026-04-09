namespace VisualProgramming.Domain.Exceptions;

public class ContainmentException : Exception
{
    public readonly object Container;
    public readonly object Item;

    public ContainmentException(object container, object item)
        : base($"Контейнир {container.GetType().Name}' не содеожит в себе {item.GetType().Name}")
    {
        Container = container;
        Item = item;
    }
}