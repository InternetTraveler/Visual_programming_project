namespace VisualProgramming.Domain.Exceptions;

public class TypePortConnectionExeption : Exception
{
    public readonly object SourcePort;
    public readonly object TargetPort;
    public readonly object Object;

    public TypePortConnectionExeption(object _object, object sourcePort, object targetPort)
        : base($"В объекте типа '{_object}' не может соедеить порты так они одинаковы")
    {
        SourcePort = sourcePort;
        TargetPort = targetPort;
        Object = _object;
    }
}
