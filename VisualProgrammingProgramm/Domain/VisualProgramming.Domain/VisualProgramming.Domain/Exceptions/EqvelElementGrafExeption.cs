namespace VisualProgramming.Domain.Exceptions;

public class EqvelElementGrafExeption : Exception
{
    public readonly object Element;
    public readonly object Object;

    public EqvelElementGrafExeption(object _object, object element)
        : base("Нельзя соеденить один и тот же объект")
    {
        Element = element;
        Object = _object;
    }
}