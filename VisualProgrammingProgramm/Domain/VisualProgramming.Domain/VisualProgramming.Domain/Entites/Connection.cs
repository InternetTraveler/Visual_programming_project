using VisualProgramming.Domain.Base;

namespace VisualProgramming.Domain.Entites;

public class Connection : Entity<Guid>
{
    public Port SourcePortId { get; private set; }
    public Port TargetPortId { get; private set; }

    public Connection(Port sourcePortId, Port targetPortId)
        : base(Guid.NewGuid())
    {
        SourcePortId = sourcePortId;
        TargetPortId = targetPortId;
    }
}
