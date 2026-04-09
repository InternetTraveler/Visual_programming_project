using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Exceptions;
using VisualProgramming.Domain.Exceptions.NullExeption;

namespace VisualProgramming.Domain.Entites;

public class Connection : Entity<Guid>
{
    public ElementGraf InElementGraf { get; private set; }
    public Port SourcePort { get; private set; }

    public ElementGraf OutElementGraf { get; private set; }
    public Port TargetPort { get; private set; }

    public Connection(Port sourcePortId, Port targetPortId, ElementGraf inElementGraf, ElementGraf outElemGraf)
        : base(Guid.NewGuid())
    {
        if (sourcePortId.TypePort == targetPortId.TypePort)
            throw new TypePortConnectionExeption(this, sourcePortId, targetPortId);

        SourcePort = sourcePortId ?? throw new ConnectionNullExeption(this, nameof(sourcePortId), typeof(Port));
        TargetPort = targetPortId ?? throw new ConnectionNullExeption(this, nameof(targetPortId), typeof(Port));
        OutElementGraf = outElemGraf ?? throw new ConnectionNullExeption(this, nameof(outElemGraf), typeof(ElementGraf));
        InElementGraf = inElementGraf ?? throw new ConnectionNullExeption(this, nameof(inElementGraf), typeof(ElementGraf));
    }

    public void UpdateInConnection(ElementGraf element, Port port)
    {
        if (port is null)
            throw new ConnectionNullExeption(this, nameof(port), typeof(Port));

        if (port.TypePort == TargetPort.TypePort)
            throw new TypePortConnectionExeption(this, port, TargetPort);

        if (element is null)
            throw new ConnectionNullExeption(this, nameof(element), typeof(ElementGraf));

        InElementGraf = element;
        SourcePort = port;
    }

    public void UpdateOutConnection(ElementGraf element, Port port)
    {
        if (port is null)
            throw new ConnectionNullExeption(this, nameof(port), typeof(Port));

        if (port.TypePort == SourcePort.TypePort)
            throw new TypePortConnectionExeption(this, SourcePort, port);

        if (element is null)
            throw new ConnectionNullExeption(this, nameof(element), typeof(ElementGraf));

        OutElementGraf = element;
        TargetPort = port;
    }
}
