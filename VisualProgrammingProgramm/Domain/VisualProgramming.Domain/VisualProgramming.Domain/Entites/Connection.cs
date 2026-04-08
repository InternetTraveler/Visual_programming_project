using VisualProgramming.Domain.Base;

namespace VisualProgramming.Domain.Entites;

public class Connection : Entity<Guid>
{
    public ElementGraf outElementGraf { get; private set; }
    public Port SourcePort { get; private set; }

    public ElementGraf OutElemGraf { get; private set; }
    public Port TargetPort { get; private set; }

    public Connection(Port sourcePortId, Port targetPortId, ElementGraf outElemGraf, ElementGraf inElementGraf)
        : base(Guid.NewGuid())
    {
        if (sourcePortId.TypePort == targetPortId.TypePort)
            throw new Exception();

        SourcePort = sourcePortId ?? throw new Exception();
        TargetPort = targetPortId ?? throw new Exception();
        OutElemGraf = outElemGraf ?? throw new Exception();
        outElementGraf = inElementGraf ?? throw new Exception();
    }

    public void UpdateInConnection(ElementGraf element, Port port)
    {
        if (port is null)
            throw new Exception();

        if (port.TypePort == TargetPort.TypePort)
            throw new Exception();

        if (element is null)
            throw new Exception();

        outElementGraf = element;
        SourcePort = port;
    }

    public void UpdateOutConnection(ElementGraf element, Port port)
    {
        if (port is null)
            throw new Exception();

        if (port.TypePort == TargetPort.TypePort)
            throw new Exception();

        if (element is null)
            throw new Exception();

        outElementGraf = element;
        TargetPort = port;
    }
}
