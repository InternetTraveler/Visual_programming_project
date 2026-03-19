using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Enum;
namespace VisualProgramming.Domain.Entites;

public class Port : Entity<Guid>
{
    public Guid NodeId { get; private set; }
    public TypePort TypePort { get; private set; }
    public string Description { get; private set; }

    public Port(Guid nodeId, TypePort typePort, string description)
        : base(Guid.NewGuid())
    {
        NodeId = nodeId;
        TypePort = typePort;
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
}
