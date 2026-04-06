using VisualProgramming.Domain.Base;

namespace VisualProgramming.Domain.Entites;

public class Connection : Entity<Guid>
{
    public ElementGraf InElementGraf { get; private set; }
    public Port SourcePortId { get; private set; }

    public ElementGraf OutElemGraf { get; private set; }
    public Port TargetPortId { get; private set; }

    public Connection(Port sourcePortId, Port targetPortId, ElementGraf outElemGraf, ElementGraf inElementGraf)
        : base(Guid.NewGuid())
    {
        SourcePortId = sourcePortId ?? throw new Exception();
        TargetPortId = targetPortId ?? throw new Exception();
        OutElemGraf = outElemGraf ?? throw new Exception();
        InElementGraf = inElementGraf ?? throw new Exception();
    }
}
