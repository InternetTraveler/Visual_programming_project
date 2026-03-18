namespace VisualProgramming.Domain.Entites;

public class Connection
{
    public Guid Id { get; private set; }
    public Port SourcePortId { get; private set; }
    public Port TargetPortId { get; private set; }

    public Connection(Port sourcePortId, Port targetPortId)
    {
        Id = Guid.NewGuid();
        SourcePortId = sourcePortId;
        TargetPortId = targetPortId;
    }
}
